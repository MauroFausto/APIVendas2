using Domain.Interfaces.Services.Clientes;
using Domain.Interfaces.Services.Vendas;
using Domain.Models.Vendas;
using Domain.Services.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly IVendaService _vendaService;
        private readonly ILogger<VendasController> _logger;

        public VendasController(
            IVendaService vendaService, 
            ILogger<VendasController> logger
        )
        {
            _vendaService = vendaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var vendas = await _vendaService.GetAllVendasAsync();
                return Ok(vendas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all vendas");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var venda = await _vendaService.GetVendaByIdAsync(id);
                if (venda == null)
                    return NotFound(new { message = "Venda not found" });
                return Ok(venda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting venda with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Venda venda)
        {
            try
            {
                await _vendaService.AddVendaAsync(venda);
                return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating venda");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Venda venda)
        {
            try
            {
                if (id != venda.Id)
                    return BadRequest(new { message = "Id mismatch" });

                await _vendaService.UpdateVendaAsync(venda);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating venda with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _vendaService.DeleteVendaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting venda with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
} 