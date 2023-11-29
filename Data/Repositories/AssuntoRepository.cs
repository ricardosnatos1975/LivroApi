using Microsoft.EntityFrameworkCore;
using LivroApi.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivroApi.Data;

namespace LivroApi.Api.Data.Repositories
{
    public class AssuntoRepository : IAssuntoRepository
    {
        private readonly LivrariaContext _context;

        public AssuntoRepository(LivrariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assunto>> ObterTodosAssuntosAsync()
        {
            return await _context.Assuntos.ToListAsync();
        }

        public async Task<Assunto> ObterAssuntoPorIdAsync(int assuntoId)
        {
            return await _context.Assuntos.FindAsync(assuntoId);
        }

        public async Task AdicionarAssuntoAsync(Assunto assunto)
        {
            _context.Assuntos.Add(assunto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAssuntoAsync(Assunto assunto)
        {
            _context.Entry(assunto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAssuntoAsync(int assuntoId)
        {
            var assunto = await _context.Assuntos.FindAsync(assuntoId);
            if (assunto != null)
            {
                _context.Assuntos.Remove(assunto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
