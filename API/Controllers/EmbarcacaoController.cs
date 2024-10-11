using Microsoft.AspNetCore.Mvc;
using RotaMarinho.Application.Services;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmbarcacao(int id)
        {
            try
            {
                var embarcacao = await _embarcacaoService.GetEmbarcacaoByIdAsync(id);
                return Ok(embarcacao);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/rotamarinho/embarcacao
        [HttpPost]
        public async Task<IActionResult> AddEmbarcacao([FromBody] EmbarcacaoDTO embarcacaoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _embarcacaoService.AddEmbarcacaoAsync(embarcacaoDTO);
                return CreatedAtAction(nameof(GetEmbarcacao), new { id = embarcacaoDTO.Id }, embarcacaoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/rotamarinho/embarcacao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmbarcacao(int id, [FromBody] EmbarcacaoDTO embarcacaoDTO)
        {
            if (id != embarcacaoDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                await _embarcacaoService.UpdateEmbarcacaoAsync(id, embarcacaoDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
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
