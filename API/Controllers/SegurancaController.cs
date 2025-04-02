using Domain.Interfaces.Services.Usuarios;
using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vendas.API.DTOs.Seguranca;
using Microsoft.AspNetCore.Http;
using Domain.Models.Base;

namespace Vendas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class SegurancaController : ControllerBase
    {
        private readonly ISegurancaService _segurancaService;
        private readonly ILogger<SegurancaController> _logger;

        public SegurancaController(ISegurancaService segurancaService, ILogger<SegurancaController> logger)
        {
            _segurancaService = segurancaService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user using email and password
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>JWT token if authentication is successful</returns>
        [HttpPost("login/email")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _segurancaService.LoginAsync(request.Email, request.Senha);
                if (result.Succeeded)
                {
                    var user = await _segurancaService.GetUserByEmailAsync(request.Email);
                    var token = await _segurancaService.GenerateJwtTokenAsync(user);
                    return Ok(new { token });
                }

                return Unauthorized(new { message = "Invalid credentials" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Authenticates a user using CPF and password
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>JWT token if authentication is successful</returns>
        [HttpPost("login/cpf")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginByCpf([FromBody] LoginCpfRequest request)
        {
            try
            {
                var result = await _segurancaService.LoginByCpfAsync(request.Cpf, request.Senha);
                if (result.Succeeded)
                {
                    var user = await _segurancaService.GetUserByCpfAsync(request.Cpf);
                    var token = await _segurancaService.GenerateJwtTokenAsync(user);
                    return Ok(new { token });
                }

                return Unauthorized(new { message = "Invalid credentials" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login by CPF");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Registers a new admin user
        /// </summary>
        /// <param name="request">Registration details</param>
        /// <returns>Success message if registration is successful</returns>
        [HttpPost("register/admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
        {
            try
            {
                var admin = new Admin 
                { 
                    UserName = request.Email,
                    Email = request.Email,
                    PasswordHash = request.Senha,
                    RegistroEntidadePessoaSistema = new EntidadePessoaSistema 
                    { 
                        Nome = request.Email,
                        DocumentoOficial = request.Cpf,
                        Telefone = "",
                        Email = request.Email
                    }
                };

                var result = await _segurancaService.RegisterAsync(admin, request.Senha);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Admin registered successfully" });
                }

                return BadRequest(new { message = "Failed to register admin", errors = result.Errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during admin registration");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Registers a new vendor user
        /// </summary>
        /// <param name="request">Registration details</param>
        /// <returns>Success message if registration is successful</returns>
        [HttpPost("register/vendedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RegisterVendedor([FromBody] RegisterRequest request)
        {
            try
            {
                var vendedor = new Vendedor 
                { 
                    UserName = request.Email,
                    Email = request.Email,
                    PasswordHash = request.Senha,
                    RegistroEntidadePessoaSistema = new EntidadePessoaSistema 
                    { 
                        Nome = request.Email,
                        DocumentoOficial = request.Cpf,
                        Telefone = "",
                        Email = request.Email
                    }
                };

                var result = await _segurancaService.RegisterAsync(vendedor, request.Senha);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Vendedor registered successfully" });
                }

                return BadRequest(new { message = "Failed to register vendedor", errors = result.Errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during vendedor registration");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
} 