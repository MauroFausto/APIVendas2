using FluentValidation;
using Domain.Models.Clientes;

namespace Domain.Validators.Clientes
{
    public class CategoriaClienteValidator : AbstractValidator<CategoriaCliente>
    {
        public CategoriaClienteValidator()
        {
            RuleFor(cc => cc.Categoria)
                .NotEmpty()
                    .WithMessage("A 'categoria' é obrigatória.")
                .NotNull()
                    .WithMessage("A 'categoria' é obrigatória.");

            RuleFor(cc => cc.Desconto)
                .NotNull()
                    .WithMessage("O 'desconto' é obrigatório.")
                .NotEmpty()
                    .WithMessage("O 'desconto' é obrigatório")
                .NotEqual(0)
                    .WithMessage("O 'desconto' não pode ser igual a 0.")
                .GreaterThan(0.1M)
                    .WithMessage("O 'desconto' precisa ser maior que 0.1");

        }
    }
}
