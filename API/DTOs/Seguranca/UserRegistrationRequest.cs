using System.ComponentModel.DataAnnotations;

namespace Vendas.API.DTOs.Seguranca
{
    public class UserRegistrationRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public required string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Tipo de usuário é obrigatório")]
        public required string UserType { get; set; } // "Admin" or "Vendedor"
    }
} 