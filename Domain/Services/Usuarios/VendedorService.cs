using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Usuarios;
using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services.Usuarios
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly UserManager<UsuarioBase> _userManager;

        public VendedorService(IVendedorRepository vendedorRepository, UserManager<UsuarioBase> userManager)
        {
            _vendedorRepository = vendedorRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Vendedor>> GetAllVendedoresAsync()
        {
            return await _vendedorRepository.GetAllAsync();
        }

        public async Task<Vendedor> GetVendedorByIdAsync(Guid id)
        {
            var vendedor = await _vendedorRepository.GetByIdAsync(id);
            if (vendedor == null)
                throw new Exception("Vendedor not found");
            return vendedor;
        }

        public async Task AddVendedorAsync(Vendedor vendedor)
        {
            vendedor.TipoUsuario = "Vendedor";

            var result = await _userManager.CreateAsync(vendedor, vendedor.PasswordHash);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create vendedor: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            await _userManager.AddToRoleAsync(vendedor, "Vendedor");
        }

        public async Task UpdateVendedorAsync(Vendedor vendedor)
        {
            var existingVendedor = await _vendedorRepository.GetByIdAsync(Guid.Parse(vendedor.Id));
            if (existingVendedor == null)
                throw new Exception("Vendedor not found");

            existingVendedor.UserName = vendedor.UserName;
            existingVendedor.Email = vendedor.Email;
            existingVendedor.PhoneNumber = vendedor.PhoneNumber;
            existingVendedor.RegistroEntidadePessoaSistema = vendedor.RegistroEntidadePessoaSistema;
            existingVendedor.CodigoVendedor = vendedor.CodigoVendedor;

            if (!string.IsNullOrEmpty(vendedor.PasswordHash))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingVendedor);
                var result = await _userManager.ResetPasswordAsync(existingVendedor, token, vendedor.PasswordHash);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to update password: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            var updateResult = await _userManager.UpdateAsync(existingVendedor);
            if (!updateResult.Succeeded)
            {
                throw new Exception($"Failed to update vendedor: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
            }
        }

        public async Task DeleteVendedorAsync(Guid id)
        {
            var vendedor = await _vendedorRepository.GetByIdAsync(id);
            if (vendedor == null)
                throw new Exception("Vendedor not found");

            var result = await _userManager.DeleteAsync(vendedor);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to delete vendedor: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task<Vendedor> GetVendedorByEmailAsync(string email)
        {
            var vendedor = await _vendedorRepository.GetByEmailAsync(email);
            if (vendedor == null)
                throw new Exception("Vendedor not found");
            return vendedor;
        }

        public async Task<Vendedor> GetVendedorByCpfAsync(string cpf)
        {
            var vendedor = await _vendedorRepository.GetByCpfAsync(cpf);
            if (vendedor == null)
                throw new Exception("Vendedor not found");
            return vendedor;
        }

        public async Task<Vendedor> GetVendedorByEmailAndSenhaAsync(string email, string senha)
        {
            var vendedor = await _vendedorRepository.GetByEmailAndSenhaAsync(email, senha);
            if (vendedor == null)
                throw new Exception("Invalid credentials");
            return vendedor;
        }

        public async Task<Vendedor> GetVendedorByCpfAndSenhaAsync(string cpf, string senha)
        {
            var vendedor = await _vendedorRepository.GetByCpfAndSenhaAsync(cpf, senha);
            if (vendedor == null)
                throw new Exception("Invalid credentials");
            return vendedor;
        }
    }
}
