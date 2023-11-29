using System.Threading.Tasks;

namespace LivroApi.Api.Services
{
    public interface IMyCustomService
    {
        Task<string> ObterInformacaoAsync();

        Task<int> RealizarOperacaoComplexaAsync(string parametro1, int parametro2);

        Task<bool> ValidarDadosAsync(DadosValidacao dados);

    }

    public class DadosValidacao
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}
