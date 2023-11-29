using System.Collections.Generic;
using System.Threading.Tasks;
using LivroApi.Api.Models;

namespace LivroApi.Api.Data.Repositories
{
    public interface IAssuntoRepository
    {
        Task<IEnumerable<Assunto>> ObterTodosAssuntosAsync();
        Task<Assunto> ObterAssuntoPorIdAsync(int assuntoId);
        Task AdicionarAssuntoAsync(Assunto assunto);
        Task AtualizarAssuntoAsync(Assunto assunto);
        Task ExcluirAssuntoAsync(int assuntoId);
    }
}
