using AutoMapper;
using LivroApi.Api.Services;
using LivroApi.Data;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.CircuitBreaker;
using System.Linq.Expressions;

public class AutorService : IAutorService
{
    private readonly ILivrariaContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<AutorService> _logger;
    private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

    public AutorService(ILivrariaContext context, IMapper mapper, ILogger<AutorService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _circuitBreaker = Policy.Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 3,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (ex, breakDelay) => OnBreak(ex, breakDelay),
                onReset: OnReset,
                onHalfOpen: OnHalfOpen
            );
    }

    private void OnBreak(Exception ex, TimeSpan breakDelay)
    {
        _logger.LogWarning($"Circuito aberto devido a erro: {ex.Message}. Tentando novamente em {breakDelay.TotalSeconds} segundos.");
    }

    private void OnReset()
    {
        _logger.LogInformation("Circuito fechado. Retentativas serão permitidas.");
    }

    private void OnHalfOpen()
    {
        _logger.LogInformation("Circuito em modo meio aberto. Tentando uma única solicitação para verificar a recuperação.");
    }

    public async Task<IEnumerable<AutorDto>> ObterTodosAutoresAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Nome")
    {
        var autores = await Policy.WrapAsync(_circuitBreaker)
            .ExecuteAsync(() => ObterAutoresAsync(pagina, itensPorPagina, ordenarPor));

        return _mapper.Map<IEnumerable<AutorDto>>(autores);
    }

    private async Task<IEnumerable<Autor>> ObterAutoresAsync(int pagina, int itensPorPagina, string ordenarPor)
    {
        return await _context.Autores
            .OrderBy(GetOrderByExpression(ordenarPor))
            .Skip((pagina - 1) * itensPorPagina)
            .Take(itensPorPagina)
            .ToListAsync();
    }

    private Expression<Func<Autor, object>> GetOrderByExpression(string ordenarPor)
    {
        return ordenarPor.ToLower() switch
        {
            "id" => autor => autor.AutorID,
            _ => autor => autor.Nome,
        };
    }

    public async Task<ServiceResult<AutorDto>> ObterAutorPorIdAsync(int autorId)
    {
        try
        {
            var autor = await _context.Autores.FindAsync(autorId);

            if (autor == null)
            {
                throw new Exception ($"Autor com ID {autorId} não encontrado.");
            }

            var autorDto = _mapper.Map<AutorDto>(autor);
            return ServiceResult<AutorDto>.Success(autorDto);
        }
        catch (Exception ex)
        {
            return await HandleCircuitBreakerAsync<AutorDto>(ex, autorId);
        }
    }

    private async Task<ServiceResult<T>> HandleCircuitBreakerAsync<T>(Exception ex, int autorId)
    {
        LogError(ex);

        if (_circuitBreaker.CircuitState == CircuitState.Open)
        {
            return ServiceResult<T>.Error($"Erro ao obter autor. O circuit breaker está aberto. Estado: {_circuitBreaker.CircuitState}");
        }

        var result = await Policy.WrapAsync(_circuitBreaker)
            .ExecuteAsync(() => ObterAutorPorIdAsync(autorId));

        if (result is ServiceResult<T> typedResult)
        {
            // Se a operação foi bem-sucedida, retornamos o resultado diretamente.
            return typedResult;
        }
        else
        {
            // Se houve um erro, retornamos o erro.
            return ServiceResult<T>.Error(result.ErrorMessage);
        }
    }


    private void LogError(Exception ex)
    {
        _logger.LogError(ex, "Erro no serviço de autor");
    }

    async Task<AutorDto> IAutorService.ObterAutorPorIdAsync(int autorId)
    {
        var autor = await ObterAutorPorIdAsync(autorId) ?? 
            throw new Exception($"Autor com ID {autorId} não encontrado.");

        return _mapper.Map<AutorDto>(autor);
    }

    public async Task<AutorDto> AdicionarAutorAsync(CriarAutorDto autorDto)
    {
        var autor = _mapper.Map<Autor>(autorDto);

        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();

        return _mapper.Map<AutorDto>(autor);
    }

    public async Task<AutorDto> AtualizarAutorAsync(int autorId, AtualizarAutorDto autorDto)
    {
        var autor = await _context.Autores.FindAsync(autorId) ?? 
            throw new Exception($"Autor com ID {autorId} não encontrado.");

        _mapper.Map(autorDto, autor);

        await _context.SaveChangesAsync();

        return _mapper.Map<AutorDto>(autor);
    }

    public async Task<AutorDto> ExcluirAutorAsync(int autorId)
    {
        var autor = await _context.Autores.FindAsync(autorId) ?? 
            throw new Exception($"Autor com ID {autorId} não encontrado.");

        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();

        return _mapper.Map<AutorDto>(autor);
    }


    public class ServiceResult<T>
{
    public bool IsSuccess { get; private set; }
    public T Data { get; private set; }
    public string ErrorMessage { get; private set; }
    public string SuccessMessage { get; private set; }

    private ServiceResult()
    {
    }

    public static ServiceResult<T> Success(T data, string successMessage = null)
    {
        return new ServiceResult<T>
        {
            IsSuccess = true,
            Data = data,
            SuccessMessage = successMessage
        };
    }

    public static ServiceResult<T> Error(string errorMessage)
    {
        return new ServiceResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}
}
