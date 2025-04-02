using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Usuarios;
using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services.Usuarios
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<UsuarioBase> _userManager;

        public AdminService(IAdminRepository adminRepository, UserManager<UsuarioBase> userManager)
        {
            _adminRepository = adminRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            return await _adminRepository.GetAllAsync();
        }

        public async Task<Admin> GetAdminByIdAsync(Guid id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            if (admin == null)
                throw new Exception("Admin not found");
            return admin;
        }

        public async Task AddAdminAsync(Admin admin)
        {
            admin.TipoUsuario = "Admin";

            var result = await _userManager.CreateAsync(admin, admin.PasswordHash);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            await _userManager.AddToRoleAsync(admin, "Admin");
        }

        public async Task UpdateAdminAsync(Admin admin)
        {
            var existingAdmin = await _adminRepository.GetByIdAsync(Guid.Parse(admin.Id));
            if (existingAdmin == null)
                throw new Exception("Admin not found");

            existingAdmin.UserName = admin.UserName;
            existingAdmin.Email = admin.Email;
            existingAdmin.PhoneNumber = admin.PhoneNumber;
            existingAdmin.RegistroEntidadePessoaSistema = admin.RegistroEntidadePessoaSistema;

            if (!string.IsNullOrEmpty(admin.PasswordHash))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingAdmin);
                var result = await _userManager.ResetPasswordAsync(existingAdmin, token, admin.PasswordHash);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to update password: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            var updateResult = await _userManager.UpdateAsync(existingAdmin);
            if (!updateResult.Succeeded)
            {
                throw new Exception($"Failed to update admin: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
            }
        }

        public async Task DeleteAdminAsync(Guid id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            if (admin == null)
                throw new Exception("Admin not found");

            var result = await _userManager.DeleteAsync(admin);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to delete admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            var admin = await _adminRepository.GetByEmailAsync(email);
            if (admin == null)
                throw new Exception("Admin not found");
            return admin;
        }

        public async Task<Admin> GetAdminByCpfAsync(string cpf)
        {
            var admin = await _adminRepository.GetByCpfAsync(cpf);
            if (admin == null)
                throw new Exception("Admin not found");
            return admin;
        }

        public async Task<Admin> GetAdminByEmailAndSenhaAsync(string email, string senha)
        {
            var admin = await _adminRepository.GetByEmailAndSenhaAsync(email, senha);
            if (admin == null)
                throw new Exception("Invalid credentials");
            return admin;
        }

        public async Task<Admin> GetAdminByCpfAndSenhaAsync(string cpf, string senha)
        {
            var admin = await _adminRepository.GetByCpfAndSenhaAsync(cpf, senha);
            if (admin == null)
                throw new Exception("Invalid credentials");
            return admin;
        }
    }
}
