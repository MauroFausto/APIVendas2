using FluentValidation;
using Domain.Models.Usuarios;

namespace Domain.Validators.Usuarios
{
    public class AdminValidator : AbstractValidator<Admin>
    {
        public AdminValidator()
        {
            RuleFor(a => a.RegistroEntidadePessoaSistema)
                .NotEmpty()
                    .WithMessage("O registro da 'entidade-pessoa-sistema' é obrigatório.")
                .NotNull()
                    .WithMessage("O registro da 'entidade-pessoa-sistema' é obrigatório.");
        }
    }
}
