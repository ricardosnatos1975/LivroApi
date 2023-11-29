using Microsoft.AspNetCore.Mvc;
using LivroApi.Api.Services;
using LivroApi.Api.Models;
using LivroApi.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivroApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService ?? throw new ArgumentNullException(nameof(livroService));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosLivros()
        {
            var livros = await _livroService.ObterTodosLivrosAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterLivroPorId(int id)
        {
            var livro = await _livroService.ObterLivroPorIdAsync(id);

            if (livro == null)
                return NotFound();

            return Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarLivro([FromBody] CriarLivroDto livroDto)
        {
            // Validar a entrada
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoLivro = await _livroService.AdicionarLivroAsync(livroDto);

            return CreatedAtAction(nameof(ObterLivroPorId), new { id = novoLivro.LivroID }, novoLivro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarLivro(int id, [FromBody] AtualizarLivroDto livroDto)
        {
            // Validar a entrada
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livroAtualizado = await _livroService.AtualizarLivroAsync(id, livroDto);

            if (livroAtualizado == null)
                return NotFound();

            return Ok(livroAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirLivro(int id)
        {
            var livroExcluido = await _livroService.ExcluirLivroAsync(id);

            if (livroExcluido == null)
                return NotFound();

            return NoContent();
        }
    }
}
