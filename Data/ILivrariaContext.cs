using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LivroApi.Api.Models;

namespace LivroApi.Data
{
    public interface ILivrariaContext : IDisposable
    {
        DbSet<Autor> Autores { get; set; }
        DbSet<Assunto> Assuntos { get; set; }
        DbSet<Livro> Livros { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
