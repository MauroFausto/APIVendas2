using Domain.Interfaces.Services.Usuarios;
using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedoresController : ControllerBase
    {
        private readonly IVendedorService _vendedorService;
        private readonly ILogger<VendedoresController> _logger;

        public VendedoresController(IVendedorService vendedorService, ILogger<VendedoresController> logger)
        {
            _vendedorService = vendedorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var vendedores = await _vendedorService.GetAllVendedoresAsync();
                return Ok(vendedores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all vendedores");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var vendedor = await _vendedorService.GetVendedorByIdAsync(id);
                if (vendedor == null)
                    return NotFound(new { message = "Vendedor not found" });
                return Ok(vendedor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting vendedor with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var vendedor = await _vendedorService.GetVendedorByEmailAsync(email);
                if (vendedor == null)
                    return NotFound(new { message = "Vendedor not found" });
                return Ok(vendedor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting vendedor by email {Email}", email);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            try
            {
                var vendedor = await _vendedorService.GetVendedorByCpfAsync(cpf);
                if (vendedor == null)
                    return NotFound(new { message = "Vendedor not found" });
                return Ok(vendedor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting vendedor by cpf {Cpf}", cpf);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string senha)
        {
            try
            {
                var vendedor = await _vendedorService.GetVendedorByEmailAndSenhaAsync(email, senha);
                if (vendedor == null)
                    return NotFound(new { message = "Invalid credentials" });
                return Ok(vendedor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in vendedor with email {Email}", email);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("login/cpf")]
        public async Task<IActionResult> LoginByCpf([FromQuery] string cpf, [FromQuery] string senha)
        {
            try
            {
                var vendedor = await _vendedorService.GetVendedorByCpfAndSenhaAsync(cpf, senha);
                if (vendedor == null)
                    return NotFound(new { message = "Invalid credentials" });
                return Ok(vendedor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in vendedor with cpf {Cpf}", cpf);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vendedor vendedor)
        {
            try
            {
                await _vendedorService.AddVendedorAsync(vendedor);
                return CreatedAtAction(nameof(GetById), new { id = vendedor.Id }, vendedor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating vendedor");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Vendedor vendedor)
        {
            try
            {
                if (id.ToString() != vendedor.Id)
                    return BadRequest(new { message = "Id mismatch" });

                await _vendedorService.UpdateVendedorAsync(vendedor);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating vendedor with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _vendedorService.DeleteVendedorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting vendedor with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
} 