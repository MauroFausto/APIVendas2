using System.ComponentModel.DataAnnotations;

namespace Vendas.API.DTOs.Seguranca
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public required string Password { get; set; }
    }
} 