using Microsoft.AspNetCore.Mvc;
using RotaMarinho.Application.Services;
using RotaMarinho.DTOs;

namespace RotaMarinho.API.Controllers
{
    [ApiController]
    [Route("api/rotamarinho/clientes")]
    public class ClienteController(IClienteService clienteService) : ControllerBase
    {
        private readonly IClienteService _clienteService = clienteService;

        // GET: api/rotamarinho/clientes
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            if (clientes == null || !clientes.Any())
            {
                return NotFound("Nenhum cliente encontrado.");
            }

            return Ok(clientes);
        }

        // GET: api/rotamarinho/clientes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            return Ok(cliente);
        }

        // GET: api/rotamarinho/clientes/nome/{nome}
        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetClienteByNomeAsync(string nome)
        {
            var cliente = await _clienteService.GetClienteByNomeAsync(nome);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            return Ok(cliente);
        }
        
        // GET: api/rotamarinho/clientes/cpf/{cpf}
        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> GetClienteByCPFAsync(string cpf)
        {
            var cliente = await _clienteService.GetClienteByCPFAsync(cpf);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            return Ok(cliente);
        }

        // POST: api/rotamarinho/clientes
        [HttpPost]
        public async Task<IActionResult> AddCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteExistente = await _clienteService.GetClienteByIdAsync(clienteDTO.Id);
            if (clienteExistente != null)
            {
                return Conflict("Cliente com este ID já existe.");
            }

            await _clienteService.AddClienteAsync(clienteDTO);
            return CreatedAtAction(nameof(AddCliente), new { id = clienteDTO.Id }, clienteDTO);
        }

        // PUT: api/rotamarinho/clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.Id)
            {
                return BadRequest("ID do cliente não corresponde.");
            }

            var clienteExistente = await _clienteService.GetClienteByIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            await _clienteService.UpdateClienteAsync(id, clienteDTO);
            return NoContent();
        }

        // DELETE: api/rotamarinho/clientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            await _clienteService.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}
