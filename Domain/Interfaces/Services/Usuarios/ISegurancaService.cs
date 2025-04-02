using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces.Services.Usuarios
{
    public interface ISegurancaService
    {
        Task<IdentityResult> LoginAsync(string email, string password);
        Task<IdentityResult> LoginByCpfAsync(string cpf, string password);
        Task<UsuarioBase> GetUserByEmailAsync(string email);
        Task<UsuarioBase> GetUserByCpfAsync(string cpf);
        Task<string> GenerateJwtTokenAsync(UsuarioBase user);
        Task<IdentityResult> RegisterAsync(UsuarioBase user, string password);
    }
} 