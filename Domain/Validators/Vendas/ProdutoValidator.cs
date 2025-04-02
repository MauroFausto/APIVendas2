using FluentValidation;
using Domain.Models.Vendas;

namespace Domain.Validators.Vendas
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator() 
        {
            RuleFor(p => p.Descricao)
                .NotNull()
                    .WithMessage("A 'Descrição' é obrigatória.")
                .NotEmpty()
                    .WithMessage("A 'Descrição' é obrigatória.");

            RuleFor(p => p.PrecoUnitario)
                .NotNull()
                    .WithMessage("O 'PrecoUnitario' é obrigatório.")
                .NotEmpty()
                    .WithMessage("O 'PrecoUnitario' é obrigatório.");
            
            RuleFor(p => p.Quantidade)
                .NotNull()
                    .WithMessage("A 'Quantidade' é obrigatória.")
                .NotEmpty()
                    .WithMessage("A 'Quantidade' é obrigatória.");
        }
    }
}
