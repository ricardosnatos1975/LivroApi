using Microsoft.AspNetCore.Mvc;
using LivroApi.Api.Services;
using LivroApi.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivroApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService ?? throw new ArgumentNullException(nameof(autorService));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAutores()
        {
            var autores = await _autorService.ObterTodosAutoresAsync();
            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterAutorPorId(int id)
        {
            var autor = await _autorService.ObterAutorPorIdAsync(id);

            if (autor == null)
                return NotFound();

            return Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAutor([FromBody] CriarAutorDto autorDto)
        {
            // Validar a entrada
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoAutor = await _autorService.AdicionarAutorAsync(autorDto);

            return CreatedAtAction(nameof(ObterAutorPorId), new { id = novoAutor.AutorID }, novoAutor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAutor(int id, [FromBody] AtualizarAutorDto autorDto)
        {
            // Validar a entrada
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var autorAtualizado = await _autorService.AtualizarAutorAsync(id, autorDto);

            if (autorAtualizado == null)
                return NotFound();

            return Ok(autorAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAutor(int id)
        {
            var autorExcluido = await _autorService.ExcluirAutorAsync(id);

            if (autorExcluido == null)
                return NotFound();

            return NoContent();
        }
    }
}
