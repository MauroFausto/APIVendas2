using Domain.Interfaces.Services.Clientes;
using Domain.Models.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasClienteController : ControllerBase
    {
        private readonly ICategoriaClienteService _categoriaClienteService;
        private readonly ILogger<CategoriasClienteController> _logger;

        public CategoriasClienteController(ICategoriaClienteService categoriaClienteService, ILogger<CategoriasClienteController> logger)
        {
            _categoriaClienteService = categoriaClienteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categorias = await _categoriaClienteService.GetAllCategoriasClienteAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all categorias cliente");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var categoria = await _categoriaClienteService.GetCategoriaClienteByIdAsync(id);
                if (categoria == null)
                    return NotFound(new { message = "Categoria Cliente not found" });
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting categoria cliente with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var categorias = await _categoriaClienteService.GetCategoriasClienteByNomeAsync(nome);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting categorias cliente by nome {Nome}", nome);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("descricao/{descricao}")]
        public async Task<IActionResult> GetByDescricao(string descricao)
        {
            try
            {
                var categorias = await _categoriaClienteService.GetCategoriasClienteByDescricaoAsync(descricao);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting categorias cliente by descricao {Descricao}", descricao);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaCliente categoria)
        {
            try
            {
                await _categoriaClienteService.AddCategoriaClienteAsync(categoria);
                return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating categoria cliente");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoriaCliente categoria)
        {
            try
            {
                if (id != categoria.Id)
                    return BadRequest(new { message = "Id mismatch" });

                await _categoriaClienteService.UpdateCategoriaClienteAsync(categoria);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating categoria cliente with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _categoriaClienteService.DeleteCategoriaClienteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting categoria cliente with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
} 