using Domain.Interfaces.Repositories;
using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Context;

namespace Data.Repositories.Usuarios
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly UserManager<UsuarioBase> _userManager;
        private readonly VendasDbContext _context;

        public VendedorRepository(UserManager<UsuarioBase> userManager, VendasDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Vendedor?> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user as Vendedor;
        }

        public async Task<IEnumerable<Vendedor>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.OfType<Vendedor>();
        }

        public async Task<Vendedor?> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user as Vendedor;
        }

        public async Task<Vendedor?> GetByCpfAsync(string cpf)
        {
            return await _context.Set<Vendedor>()
                .FirstOrDefaultAsync(v => v .RegistroEntidadePessoaSistema.DocumentoOficial == cpf);
        }   

        public async Task<Vendedor?> GetByEmailAndSenhaAsync(string email, string senha)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(user, senha);
            return isValid ? user as Vendedor : null;
        }

        public async Task<Vendedor?> GetByCpfAndSenhaAsync(string cpf, string senha)
        {
            var vendedor = await _context.Set<Vendedor>()
                .FirstOrDefaultAsync(v => v.RegistroEntidadePessoaSistema.DocumentoOficial == cpf);
            
            if (vendedor == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(vendedor, senha);
            return isValid ? vendedor : null;
        }

        public async Task AddAsync(Vendedor vendedor)
        {
            var result = await _userManager.CreateAsync(vendedor);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create vendedor: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task UpdateAsync(Vendedor vendedor)
        {
            var result = await _userManager.UpdateAsync(vendedor);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to update vendedor: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var vendedor = await GetByIdAsync(id);
            if (vendedor == null) return;

            var result = await _userManager.DeleteAsync(vendedor);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to delete vendedor: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
