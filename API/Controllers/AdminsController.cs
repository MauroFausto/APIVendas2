using Domain.Interfaces.Services.Usuarios;
using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;

namespace Vendas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminsController> _logger;

        public AdminsController(IAdminService adminService, ILogger<AdminsController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all admin users
        /// </summary>
        /// <returns>List of admin users</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Admin>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAll()
        {
            try
            {
                var admins = await _adminService.GetAllAdminsAsync();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all admins");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Gets an admin user by ID
        /// </summary>
        /// <param name="id">The ID of the admin user</param>
        /// <returns>The admin user if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Admin>> GetById(Guid id)
        {
            try
            {
                var admin = await _adminService.GetAdminByIdAsync(id);
                if (admin == null)
                    return NotFound(new { message = "Admin not found" });

                return Ok(admin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting admin by ID {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Gets an admin user by email
        /// </summary>
        /// <param name="email">The email of the admin user</param>
        /// <returns>The admin user if found</returns>
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Admin>> GetByEmail(string email)
        {
            try
            {
                var admin = await _adminService.GetAdminByEmailAsync(email);
                if (admin == null)
                    return NotFound(new { message = "Admin not found" });
                return Ok(admin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting admin by email {Email}", email);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Gets an admin user by CPF
        /// </summary>
        /// <param name="cpf">The CPF of the admin user</param>
        /// <returns>The admin user if found</returns>
        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Admin>> GetByCpf(string cpf)
        {
            try
            {
                var admin = await _adminService.GetAdminByCpfAsync(cpf);
                if (admin == null)
                    return NotFound(new { message = "Admin not found" });
                return Ok(admin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting admin by cpf {Cpf}", cpf);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Creates a new admin user
        /// </summary>
        /// <param name="admin">The admin user to create</param>
        /// <returns>The created admin user</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Admin>> Create([FromBody] Admin admin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _adminService.AddAdminAsync(admin);
                return CreatedAtAction(nameof(GetById), new { id = admin.Id }, admin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating admin");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Updates an existing admin user
        /// </summary>
        /// <param name="id">The ID of the admin user to update</param>
        /// <param name="admin">The updated admin user data</param>
        /// <returns>No content if successful</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(Guid id, [FromBody] Admin admin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id.ToString() != admin.Id)
                    return BadRequest(new { message = "Id mismatch" });

                await _adminService.UpdateAdminAsync(admin);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating admin with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Deletes an admin user
        /// </summary>
        /// <param name="id">The ID of the admin user to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _adminService.DeleteAdminAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting admin with id {Id}", id);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
} 
 