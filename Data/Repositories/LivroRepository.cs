using Microsoft.EntityFrameworkCore;
using LivroApi.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivroApi.Data;

namespace LivroApi.Api.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly LivrariaContext _context;

        public LivroRepository(LivrariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livro>> ObterTodosLivrosAsync()
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Assunto)
                .ToListAsync();
        }

        public async Task<Livro> ObterLivroPorIdAsync(int livroId)
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Assunto)
                .FirstOrDefaultAsync(l => l.LivroID == livroId);
        }

        public async Task AdicionarLivroAsync(Livro livro)
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarLivroAsync(Livro livro)
        {
            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirLivroAsync(int livroId)
        {
            var livro = await _context.Livros.FindAsync(livroId);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }
    }
}
