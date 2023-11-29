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
    public class AssuntosController : ControllerBase
    {
        private readonly IAssuntoService _assuntoService;

        public AssuntosController(IAssuntoService assuntoService)
        {
            _assuntoService = assuntoService ?? throw new ArgumentNullException(nameof(assuntoService));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAssuntos()
        {
            var assuntos = await _assuntoService.ObterTodosAssuntosAsync();
            return Ok(assuntos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterAssuntoPorId(int id)
        {
            var assunto = await _assuntoService.ObterAssuntoPorIdAsync(id);

            if (assunto == null)
                return NotFound();

            return Ok(assunto);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAssunto([FromBody] CriarAssuntoDto assuntoDto)
        {
            // Validar a entrada
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoAssunto = await _assuntoService.AdicionarAssuntoAsync(assuntoDto);

            return CreatedAtAction(nameof(ObterAssuntoPorId), new { id = novoAssunto.AssuntoID }, novoAssunto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAssunto(int id, [FromBody] AtualizarAssuntoDto assuntoDto)
        {
            // Validar a entrada
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var assuntoAtualizado = await _assuntoService.AtualizarAssuntoAsync(id, assuntoDto);

            if (assuntoAtualizado == null)
                return NotFound();

            return Ok(assuntoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAssunto(int id)
        {
            var assuntoExcluido = await _assuntoService.ExcluirAssuntoAsync(id);

            if (assuntoExcluido == null)
                return NotFound();

            return NoContent();
        }
    }
}
