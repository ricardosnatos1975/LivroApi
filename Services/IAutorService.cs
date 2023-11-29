using System.Collections.Generic;
using System.Threading.Tasks;
using LivroApi.Api.Models;
using LivroApi.Api.Models.Dtos;

namespace LivroApi.Api.Services
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> ObterTodosAutoresAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Nome");
        Task<AutorDto> ObterAutorPorIdAsync(int autorId);
        Task<AutorDto> AdicionarAutorAsync(CriarAutorDto autorDto);
        Task<AutorDto> AtualizarAutorAsync(int autorId, AtualizarAutorDto autorDto);
        Task<AutorDto> ExcluirAutorAsync(int autorId);
    }
}
