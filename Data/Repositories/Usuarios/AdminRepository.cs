using Domain.Interfaces.Repositories;
using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Context;

namespace Data.Repositories.Usuarios
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<UsuarioBase> _userManager;
        private readonly VendasDbContext _context;

        public AdminRepository(UserManager<UsuarioBase> userManager, VendasDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Admin?> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user as Admin;
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.OfType<Admin>();
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user as Admin;
        }

        public async Task<Admin?> GetByCpfAsync(string cpf)
        {
            return await _context.Set<Admin>()
                .FirstOrDefaultAsync(a => a.RegistroEntidadePessoaSistema.DocumentoOficial == cpf);
        }

        public async Task<Admin?> GetByEmailAndSenhaAsync(string email, string senha)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(user, senha);
            return isValid ? user as Admin : null;
        }

        public async Task<Admin?> GetByCpfAndSenhaAsync(string cpf, string senha)
        {
            var admin = await _context.Set<Admin>()
                .FirstOrDefaultAsync(a => a.RegistroEntidadePessoaSistema.DocumentoOficial == cpf);
            
            if (admin == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(admin, senha);
            return isValid ? admin : null;
        }

        public async Task AddAsync(Admin admin)
        {
            var result = await _userManager.CreateAsync(admin);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task UpdateAsync(Admin admin)
        {
            var result = await _userManager.UpdateAsync(admin);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to update admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var admin = await GetByIdAsync(id);
            if (admin == null) return;

            var result = await _userManager.DeleteAsync(admin);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to delete admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
