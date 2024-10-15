using Microsoft.AspNetCore.Mvc;
using RotaMarinho.Application.Services;
using RotaMarinho.Domain.Entities;
using RotaMarinho.DTOs;

namespace RotaMarinho.API.Controllers
{
    [ApiController]
    [Route("api/rotamarinho/embarcacao")]
    public class EmbarcacaoController(IEmbarcacaoService embarcacaoService) : ControllerBase
    {
        private readonly IEmbarcacaoService _embarcacaoService = embarcacaoService;

        // GET: api/rotamarinho/embarcacao
        [HttpGet]
        public async Task<IActionResult> GetEmbarcacoes()
        {
            var embarcacoes = await _embarcacaoService.GetAllEmbarcacoesAsync();
            if (embarcacoes == null || !embarcacoes.Any())
            {
                return NotFound("Nenhuma Emabarcação encotrada.");
            }

            return Ok(embarcacoes);
        }

        // GET: api/rotamarinho/embarcacao/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmbarcacao(int id)
        {
            var embarcacao = await _embarcacaoService.GetEmbarcacaoByIdAsync(id);
            if (embarcacao == null)
            {
                return NotFound("Embarcação não encontrada.");
            }

            return Ok(embarcacao);
        }
        
        // GET: api/rotamarinho/embarcacao/{matricula}
        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetEmbarcacao(string matricula)
        {
            var embarcacao = await _embarcacaoService.GetEmbarcacaoByMatriculaAsync(matricula);
            if (embarcacao == null)
            {
                return NotFound("Embarcação não encontrada.");
            }

            return Ok(embarcacao);
        }

        // POST: api/rotamarinho/embarcacao
        [HttpPost]
        public async Task<IActionResult> AddEmbarcacao([FromBody] EmbarcacaoDTO embarcacaoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var embarcacaoExistente = _embarcacaoService.GetEmbarcacaoByIdAsync(embarcacaoDTO.Id);
            if (embarcacaoExistente != null)
            {
                return Conflict("Embarcação com este ID já existe");
            }

            await _embarcacaoService.AddEmbarcacaoAsync(embarcacaoDTO);
            return CreatedAtAction(nameof(AddEmbarcacao), new { id = embarcacaoDTO.Id }, embarcacaoDTO);
        }

        // PUT: api/rotamarinho/embarcacao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmbarcacao(int id, [FromBody] EmbarcacaoDTO embarcacaoDTO)
        {
            if (id != embarcacaoDTO.Id)
            {
                return BadRequest();
            }

            var embarcacaoExistente = await _embarcacaoService.GetEmbarcacaoByIdAsync(id);
            if (embarcacaoExistente == null)
            {
                return NotFound("Embarcação não encontrada");
            }

            await _embarcacaoService.UpdateEmbarcacaoAsync(id, embarcacaoDTO);
            return NoContent();
        }

        // DELETE: api/rotamarinho/embarcacao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmbarcacao(int id)
        {
            var embarcacao = await _embarcacaoService.GetEmbarcacaoByIdAsync(id);
            if (embarcacao == null)
            {
                return NotFound("Embarcação não encontrada.");
            }

            await _embarcacaoService.DeleteEmbarcacaoAsync(id);
            
            return NoContent();
        }
    }
}
