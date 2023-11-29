using System.Collections.Generic;
using System.Threading.Tasks;
using LivroApi.Api.Models;
using LivroApi.Api.Models.Dtos;

namespace LivroApi.Api.Services
{
    public interface IAssuntoService
    {
        Task<IEnumerable<AssuntoDto>> ObterTodosAssuntosAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Descricao");
        Task<AssuntoDto> ObterAssuntoPorIdAsync(int assuntoId);
        Task<AssuntoDto> AdicionarAssuntoAsync(CriarAssuntoDto assuntoDto);
        Task<AssuntoDto> AtualizarAssuntoAsync(int assuntoId, AtualizarAssuntoDto assuntoDto);
        Task<AssuntoDto> ExcluirAssuntoAsync(int assuntoId);
    }
}
