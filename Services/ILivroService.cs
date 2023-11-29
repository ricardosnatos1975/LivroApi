using System.Collections.Generic;
using System.Threading.Tasks;
using LivroApi.Api.Models;
using LivroApi.Api.Models.Dtos;

namespace LivroApi.Api.Services
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroDto>> ObterTodosLivrosAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Nome");
        Task<LivroDto> ObterLivroPorIdAsync(int livroId);
        Task<LivroDto> AdicionarLivroAsync(CriarLivroDto livroDto);
        Task<LivroDto> AtualizarLivroAsync(int livroId, AtualizarLivroDto livroDto);
        Task<LivroDto> ExcluirLivroAsync(int livroId);
    }
}
