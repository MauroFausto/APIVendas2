using Domain.Interfaces.Services.Vendas;
using Domain.Models.Vendas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IProdutoService produtoService, ILogger<ProdutosController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var produtos = await _produtoService.GetAllProdutosAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all produtos");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var produto = await _produtoService.GetProdutoByIdAsync(id);
                if (produto == null)
                    return NotFound(new { message = "Produto not found" });
                return Ok(produto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting produto with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Produto produto)
        {
            try
            {
                await _produtoService.AddProdutoAsync(produto);
                return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating produto");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Produto produto)
        {
            try
            {
                if (id != produto.Id)
                    return BadRequest(new { message = "Id mismatch" });

                await _produtoService.UpdateProdutoAsync(produto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating produto with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _produtoService.DeleteProdutoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting produto with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
} 