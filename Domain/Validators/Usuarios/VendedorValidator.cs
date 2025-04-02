using FluentValidation;
using Domain.Models.Usuarios;

namespace Domain.Validators.Usuarios
{
    public class VendedorValidator : AbstractValidator<Vendedor>
    {
        public VendedorValidator()
        {
            RuleFor(v => v.RegistroEntidadePessoaSistema)
                .NotEmpty()
                    .WithMessage("O registro da 'entidade-pessoa-sistema' é obrigatório.")
                .NotNull()
                    .WithMessage("O registro da 'entidade-pessoa-sistema' é obrigatório.");
        }
    }
}
