using System.Collections.Generic;
using System.Threading.Tasks;
using LivroApi.Api.Models;

namespace LivroApi.Api.Data.Repositories
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ObterTodosAutoresAsync();
        Task<Autor> ObterAutorPorIdAsync(int autorId);
        Task AdicionarAutorAsync(Autor autor);
        Task AtualizarAutorAsync(Autor autor);
        Task ExcluirAutorAsync(int autorId);
    }
}
