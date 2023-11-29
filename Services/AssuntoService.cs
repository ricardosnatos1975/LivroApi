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
    public class AssuntoService : IAssuntoService
    {
        private readonly ILivrariaContext _context;
        private readonly IMapper _mapper;

        public AssuntoService(ILivrariaContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

public async Task<IEnumerable<AssuntoDto>> ObterTodosAssuntosAsync(int pagina = 1, int itensPorPagina = 10, string ordenarPor = "Descricao")
{
    var propriedadeOrdenacao = typeof(AssuntoDto).GetProperty(ordenarPor);

    if (propriedadeOrdenacao == null)
    {
        throw new ArgumentException($"Propriedade de ordenação inválida: {ordenarPor}", nameof(ordenarPor));
    }

    var assuntos = await _context.Assuntos
        .OrderBy(a => propriedadeOrdenacao.GetValue(a))
        .Skip((pagina - 1) * itensPorPagina)
        .Take(itensPorPagina)
        .ToListAsync();

    return _mapper.Map<IEnumerable<AssuntoDto>>(assuntos);
}


        public async Task<AssuntoDto> ObterAssuntoPorIdAsync(int assuntoId)
        {
            var assunto = await _context.Assuntos.FindAsync(assuntoId);

            if (assunto == null)
            {
                // Lançar uma exceção se o assunto não for encontrado
                throw new NotFoundException($"Assunto com ID {assuntoId} não encontrado.");
            }

            return _mapper.Map<AssuntoDto>(assunto);
        }

        public async Task<AssuntoDto> AdicionarAssuntoAsync(CriarAssuntoDto assuntoDto)
        {
            var assunto = _mapper.Map<Assunto>(assuntoDto);
            _context.Assuntos.Add(assunto);
            await _context.SaveChangesAsync();
            return _mapper.Map<AssuntoDto>(assunto);
        }

        public async Task<AssuntoDto> AtualizarAssuntoAsync(int assuntoId, AtualizarAssuntoDto assuntoDto)
        {
            var assunto = await _context.Assuntos.FindAsync(assuntoId);

            if (assunto == null)
            {
                // Lançar uma exceção se o assunto não for encontrado
                throw new NotFoundException($"Assunto com ID {assuntoId} não encontrado.");
            }

            _mapper.Map(assuntoDto, assunto);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssuntoDto>(assunto);
        }

        public async Task<AssuntoDto> ExcluirAssuntoAsync(int assuntoId)
        {
            var assunto = await _context.Assuntos.FindAsync(assuntoId);

            if (assunto == null)
            {
                // Lançar uma exceção se o assunto não for encontrado
                throw new NotFoundException($"Assunto com ID {assuntoId} não encontrado.");
            }

            _context.Assuntos.Remove(assunto);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssuntoDto>(assunto);
        }

    }
}
