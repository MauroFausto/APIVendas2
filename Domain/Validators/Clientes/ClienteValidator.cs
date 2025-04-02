using FluentValidation;
using Domain.Models.Clientes;

namespace Domain.Validators.Clientes
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.CategoriaCliente)
                .NotNull()
                    .WithMessage("A 'categoria-cliente' é obrigatória.")
                .NotEmpty()
                    .WithMessage("A 'categoria-cliente' é obrigatória.");
        }
    }
}
