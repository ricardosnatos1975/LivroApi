using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LivroApi.Api.Models;
using LivroApi.Api.Models.Dtos;
using LivroApi.Data;

namespace LivroApi.Api.Services
{
    public class AutorService : IAutorService
    {
        private readonly ILivrariaContext _context;
        private readonly IMapper _mapper;

        public AutorService(ILivrariaContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<AutorDto>> ObterTodosAutoresAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Nome")
        {
            var autores = await _context.Autores
                .OrderBy(GetOrderByExpression(ordenarPor))
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AutorDto>>(autores);
        }

        public async Task<AutorDto> ObterAutorPorIdAsync(int autorId)
        {
            var autor = await _context.Autores.FindAsync(autorId);

            if (autor == null)
            {
                // Tratar o caso em que o autor não é encontrado
                return null;
            }

            return _mapper.Map<AutorDto>(autor);
        }

        public async Task<AutorDto> AdicionarAutorAsync(CriarAutorDto autorDto)
        {
            var autor = _mapper.Map<Autor>(autorDto);
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return _mapper.Map<AutorDto>(autor);
        }

        public async Task<AutorDto> AtualizarAutorAsync(int autorId, AtualizarAutorDto autorDto)
        {
            var autor = await _context.Autores.FindAsync(autorId);

            if (autor == null)
            {
                // Tratar o caso em que o autor não é encontrado
                return null;
            }

            _mapper.Map(autorDto, autor);
            await _context.SaveChangesAsync();

            return _mapper.Map<AutorDto>(autor);
        }

        public async Task<AutorDto> ExcluirAutorAsync(int autorId)
        {
            var autor = await _context.Autores.FindAsync(autorId);

            if (autor == null)
            {
                // Tratar o caso em que o autor não é encontrado
                return null;
            }

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return _mapper.Map<AutorDto>(autor);
        }

        private Expression<Func<Autor, object>> GetOrderByExpression(string ordenarPor)
        {
            switch (ordenarPor.ToLower())
            {
                case "nome":
                    return autor => autor.Nome;
                case "id":
                    return autor => autor.AutorID;
                default:
                    return autor => autor.Nome;
            }
        }
    }
}
