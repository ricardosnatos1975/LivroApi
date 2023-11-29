using System.Collections.Generic;
using System.Threading.Tasks;
using LivroApi.Api.Models;

namespace LivroApi.Api.Data.Repositories
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ObterTodosLivrosAsync();
        Task<Livro> ObterLivroPorIdAsync(int livroId);
        Task AdicionarLivroAsync(Livro livro);
        Task AtualizarLivroAsync(Livro livro);
        Task ExcluirLivroAsync(int livroId);
    }
}
