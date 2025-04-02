using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services.Clientes;
using Domain.Models.Clientes;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(IClienteService clienteService, ILogger<ClientesController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clientes = await _clienteService.GetAllClientesAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all clientes");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id);
                if (cliente == null)
                    return NotFound(new { message = "Cliente not found" });
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting cliente with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var clientes = await _clienteService.GetClienteByNomeAsync(nome);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting clientes by nome {Nome}", nome);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            try
            {
                var clientes = await _clienteService.GetClienteByCpfAsync(cpf);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting clientes by cpf {Cpf}", cpf);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var clientes = await _clienteService.GetClienteByEmailAsync(email);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting clientes by email {Email}", email);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("telefone/{telefone}")]
        public async Task<IActionResult> GetByTelefone(string telefone)
        {
            try
            {
                var clientes = await _clienteService.GetClienteByTelefoneAsync(telefone);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting clientes by telefone {Telefone}", telefone);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("categoria/{categoriaClienteId}")]
        public async Task<IActionResult> GetByCategoriaClienteId(Guid categoriaClienteId)
        {
            try
            {
                var clientes = await _clienteService.GetClientesByCategoriaClienteIdAsync(categoriaClienteId);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting clientes by categoriaClienteId {CategoriaClienteId}", categoriaClienteId);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            try
            {
                await _clienteService.AddClienteAsync(cliente);
                return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating cliente");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Cliente cliente)
        {
            try
            {
                if (id != cliente.Id)
                    return BadRequest(new { message = "Id mismatch" });

                await _clienteService.UpdateClienteAsync(cliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating cliente with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _clienteService.DeleteClienteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting cliente with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
} 