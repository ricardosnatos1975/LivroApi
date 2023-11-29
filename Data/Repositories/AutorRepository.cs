using Microsoft.EntityFrameworkCore;
using LivroApi.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivroApi.Data;

namespace LivroApi.Api.Data.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly LivrariaContext _context;

        public AutorRepository(LivrariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Autor>> ObterTodosAutoresAsync()
        {
            return await _context.Autores.ToListAsync();
        }

        public async Task<Autor> ObterAutorPorIdAsync(int autorId)
        {
            return await _context.Autores.FindAsync(autorId);
        }

        public async Task AdicionarAutorAsync(Autor autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAutorAsync(Autor autor)
        {
            _context.Entry(autor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAutorAsync(int autorId)
        {
            var autor = await _context.Autores.FindAsync(autorId);
            if (autor != null)
            {
                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
