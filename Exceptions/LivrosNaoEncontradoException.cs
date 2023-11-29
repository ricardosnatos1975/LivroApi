using AutoMapper;
using LivroApi.Api.Models;
using LivroApi.Api.Models.Dtos;
using LivroApi.Api.Services;
using LivroApi.Data;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.CircuitBreaker;
using System.Linq.Expressions;

public class LivroService : ILivroService
{
    private readonly ILivrariaContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<LivroService> _logger;
    private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

    public LivroService(ILivrariaContext context, IMapper mapper, ILogger<LivroService> logger)
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

    public async Task<IEnumerable<LivroDto>> ObterTodosLivrosAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Nome")
    {
        var Livros = await Policy.WrapAsync(_circuitBreaker)
            .ExecuteAsync(() => ObterLivrosAsync(pagina, itensPorPagina, ordenarPor));

        return _mapper.Map<IEnumerable<LivroDto>>(Livros);
    }

    private async Task<IEnumerable<Livro>> ObterLivrosAsync(int pagina, int itensPorPagina, string ordenarPor)
    {
        return await _context.Livros
            .OrderBy(GetOrderByExpression(ordenarPor))
            .Skip((pagina - 1) * itensPorPagina)
            .Take(itensPorPagina)
            .ToListAsync();
    }

    private Expression<Func<Livro, object>> GetOrderByExpression(string ordenarPor)
    {
        return ordenarPor.ToLower() switch
        {
            "id" => Livro => Livro.LivroID,
            _ => Livro => Livro.Titulo,
        };
    }

    public async Task<ServiceResult<LivroDto>> ObterLivroPorIdAsync(int LivroId)
    {
        try
        {
            var Livro = await _context.Livros.FindAsync(LivroId);

            if (Livro == null)
            {
                throw new Exception($"Livro com ID {LivroId} não encontrado.");
            }

            var LivroDto = _mapper.Map<LivroDto>(Livro);
            return ServiceResult<LivroDto>.Success(LivroDto);
        }
        catch (Exception ex)
        {
            return await HandleCircuitBreakerAsync<LivroDto>(ex, LivroId);
        }
    }

    private async Task<ServiceResult<T>> HandleCircuitBreakerAsync<T>(Exception ex, int LivroId)
    {
        LogError(ex);

        if (_circuitBreaker.CircuitState == CircuitState.Open)
        {
            return ServiceResult<T>.Error($"Erro ao obter Livro. O circuit breaker está aberto. Estado: {_circuitBreaker.CircuitState}");
        }

        var result = await Policy.WrapAsync(_circuitBreaker)
            .ExecuteAsync(() => ObterLivroPorIdAsync(LivroId));

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
        _logger.LogError(ex, "Erro no serviço de Livro");
    }

    async Task<LivroDto> ILivroService.ObterLivroPorIdAsync(int LivroId)
    {
        var Livro = await ObterLivroPorIdAsync(LivroId) ??
            throw new Exception($"Livro com ID {LivroId} não encontrado.");

        return _mapper.Map<LivroDto>(Livro);
    }

    public async Task<LivroDto> AdicionarLivroAsync(CriarLivroDto LivroDto)
    {
        var Livro = _mapper.Map<Livro>(LivroDto);

        _context.Livros.Add(Livro);
        await _context.SaveChangesAsync();

        return _mapper.Map<LivroDto>(Livro);
    }

    public async Task<LivroDto> AtualizarLivroAsync(int LivroId, AtualizarLivroDto LivroDto)
    {
        var Livro = await _context.Livros.FindAsync(LivroId) ??
            throw new Exception($"Livro com ID {LivroId} não encontrado.");

        _mapper.Map(LivroDto, Livro);

        await _context.SaveChangesAsync();

        return _mapper.Map<LivroDto>(Livro);
    }

    public async Task<LivroDto> ExcluirLivroAsync(int LivroId)
    {
        var Livro = await _context.Livros.FindAsync(LivroId) ??
            throw new Exception($"Livro com ID {LivroId} não encontrado.");

        _context.Livros.Remove(Livro);
        await _context.SaveChangesAsync();

        return _mapper.Map<LivroDto>(Livro);
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
