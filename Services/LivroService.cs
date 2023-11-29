using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LivroApi.Api.Models;
using LivroApi.Api.Models.Dtos;
using LivroApi.Api.Exceptions;
using LivroApi.Data;

namespace LivroApi.Api.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivrariaContext _context;
        private readonly IMapper _mapper;

        public LivroService(ILivrariaContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<LivroDto>> ObterTodosLivrosAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Descricao")
        {
            var propriedadeOrdenacao = typeof(LivroDto).GetProperty(ordenarPor);

            if (propriedadeOrdenacao == null)
            {
                throw new ArgumentException($"Propriedade de ordenação inválida: {ordenarPor}", nameof(ordenarPor));
            }

            var Livros = await _context.Livros
                .OrderBy(a => propriedadeOrdenacao.GetValue(a))
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LivroDto>>(Livros);
        }


        public async Task<LivroDto> ObterLivroPorIdAsync(int LivroId)
        {
            var Livro = await _context.Livros.FindAsync(LivroId);

            if (Livro == null)
            {
                // Lançar uma exceção se o Livro não for encontrado
                throw new NotFoundException($"Livro com ID {LivroId} não encontrado.");
            }

            return _mapper.Map<LivroDto>(Livro);
        }

        public async Task<LivroDto> AdicionarLivroAsync(CriarLivroDto LivroDto)
        {
            var Livro = _mapper.Map<Livro>(LivroDto);
            _context.Livros.Add(Livro);
            await _context.SaveChangesAsync();
            return _mapper.Map<LivroDto>(Livro);
        }

        public async Task<LivroDto> AtualizarLivroAsync(int LivroId, AtualizarLivroDto LivroDto)
        {
            var Livro = await _context.Livros.FindAsync(LivroId);

            if (Livro == null)
            {
                // Lançar uma exceção se o Livro não for encontrado
                throw new NotFoundException($"Livro com ID {LivroId} não encontrado.");
            }

            _mapper.Map(LivroDto, Livro);
            await _context.SaveChangesAsync();

            return _mapper.Map<LivroDto>(Livro);
        }

        public async Task<LivroDto> ExcluirLivroAsync(int LivroId)
        {
            var Livro = await _context.Livros.FindAsync(LivroId);

            if (Livro == null)
            {
                // Lançar uma exceção se o Livro não for encontrado
                throw new NotFoundException($"Livro com ID {LivroId} não encontrado.");
            }

            _context.Livros.Remove(Livro);
            await _context.SaveChangesAsync();

            return _mapper.Map<LivroDto>(Livro);
        }

    }
}
