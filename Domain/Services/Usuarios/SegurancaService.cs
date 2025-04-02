using Domain.Interfaces.Services.Usuarios;
using Domain.Models.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Usuarios
{
    public class SegurancaService : ISegurancaService
    {
        private readonly UserManager<UsuarioBase> _userManager;
        private readonly SignInManager<UsuarioBase> _signInManager;
        private readonly IConfiguration _configuration;

        public SegurancaService(
            UserManager<UsuarioBase> userManager,
            SignInManager<UsuarioBase> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid credentials" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = "Invalid credentials" });
        }

        public async Task<IdentityResult> LoginByCpfAsync(string cpf, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RegistroEntidadePessoaSistema.DocumentoOficial == cpf);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid credentials" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = "Invalid credentials" });
        }

        public async Task<UsuarioBase> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<UsuarioBase> GetUserByCpfAsync(string cpf)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.RegistroEntidadePessoaSistema.DocumentoOficial == cpf);
        }

        public async Task<string> GenerateJwtTokenAsync(UsuarioBase user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserType", user.GetType().Name)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"])),
                signingCredentials: creds
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> RegisterAsync(UsuarioBase user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}