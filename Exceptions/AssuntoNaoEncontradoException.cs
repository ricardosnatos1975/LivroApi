using AutoMapper;
using LivroApi.Api.Models;
using LivroApi.Api.Models.Dtos;
using LivroApi.Api.Services;
using LivroApi.Data;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.CircuitBreaker;
using System.Linq.Expressions;

public class AssuntoService : IAssuntoService
{
    private readonly ILivrariaContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<AssuntoService> _logger;
    private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

    public AssuntoService(ILivrariaContext context, IMapper mapper, ILogger<AssuntoService> logger)
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

    public async Task<IEnumerable<AssuntoDto>> ObterTodosAssuntosAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Nome")
    {
        var Assuntos = await Policy.WrapAsync(_circuitBreaker)
            .ExecuteAsync(() => ObterAssuntosAsync(pagina, itensPorPagina, ordenarPor));

        return _mapper.Map<IEnumerable<AssuntoDto>>(Assuntos);
    }

    private async Task<IEnumerable<Assunto>> ObterAssuntosAsync(int pagina, int itensPorPagina, string ordenarPor)
    {
        return await _context.Assuntos
            .OrderBy(GetOrderByExpression(ordenarPor))
            .Skip((pagina - 1) * itensPorPagina)
            .Take(itensPorPagina)
            .ToListAsync();
    }

    private Expression<Func<Assunto, object>> GetOrderByExpression(string ordenarPor)
    {
        return ordenarPor.ToLower() switch
        {
            "id" => Assunto => Assunto.AssuntoID,
            _ => Assunto => Assunto.Descricao,
        };
    }

    public async Task<ServiceResult<AssuntoDto>> ObterAssuntoPorIdAsync(int AssuntoId)
    {
        try
        {
            var Assunto = await _context.Assuntos.FindAsync(AssuntoId);

            if (Assunto == null)
            {
                throw new Exception($"Assunto com ID {AssuntoId} não encontrado.");
            }

            var AssuntoDto = _mapper.Map<AssuntoDto>(Assunto);
            return ServiceResult<AssuntoDto>.Success(AssuntoDto);
        }
        catch (Exception ex)
        {
            return await HandleCircuitBreakerAsync<AssuntoDto>(ex, AssuntoId);
        }
    }

    private async Task<ServiceResult<T>> HandleCircuitBreakerAsync<T>(Exception ex, int AssuntoId)
    {
        LogError(ex);

        if (_circuitBreaker.CircuitState == CircuitState.Open)
        {
            return ServiceResult<T>.Error($"Erro ao obter Assunto. O circuit breaker está aberto. Estado: {_circuitBreaker.CircuitState}");
        }

        var result = await Policy.WrapAsync(_circuitBreaker)
            .ExecuteAsync(() => ObterAssuntoPorIdAsync(AssuntoId));

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
        _logger.LogError(ex, "Erro no serviço de Assunto");
    }

    async Task<AssuntoDto> IAssuntoService.ObterAssuntoPorIdAsync(int AssuntoId)
    {
        var Assunto = await ObterAssuntoPorIdAsync(AssuntoId) ??
            throw new Exception($"Assunto com ID {AssuntoId} não encontrado.");

        return _mapper.Map<AssuntoDto>(Assunto);
    }

    public async Task<AssuntoDto> AdicionarAssuntoAsync(CriarAssuntoDto AssuntoDto)
    {
        var Assunto = _mapper.Map<Assunto>(AssuntoDto);

        _context.Assuntos.Add(Assunto);
        await _context.SaveChangesAsync();

        return _mapper.Map<AssuntoDto>(Assunto);
    }

    public async Task<AssuntoDto> AtualizarAssuntoAsync(int AssuntoId, AtualizarAssuntoDto AssuntoDto)
    {
        var Assunto = await _context.Assuntos.FindAsync(AssuntoId) ??
            throw new Exception($"Assunto com ID {AssuntoId} não encontrado.");

        _mapper.Map(AssuntoDto, Assunto);

        await _context.SaveChangesAsync();

        return _mapper.Map<AssuntoDto>(Assunto);
    }

    public async Task<AssuntoDto> ExcluirAssuntoAsync(int AssuntoId)
    {
        var Assunto = await _context.Assuntos.FindAsync(AssuntoId) ??
            throw new Exception($"Assunto com ID {AssuntoId} não encontrado.");

        _context.Assuntos.Remove(Assunto);
        await _context.SaveChangesAsync();

        return _mapper.Map<AssuntoDto>(Assunto);
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
