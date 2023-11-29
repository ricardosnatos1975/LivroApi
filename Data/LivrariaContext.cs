using Microsoft.EntityFrameworkCore;
using LivroApi.Api.Models;

namespace LivroApi.Data
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext(DbContextOptions<LivrariaContext> options) : base(options) { }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Livro> Livros { get; set; }
    }
}

