using Domain.Models.Vendas;
using FluentValidation;

namespace Domain.Validators.Vendas
{
    public class VendaValidator : AbstractValidator<Venda>
    {
        public VendaValidator()
        {
            RuleFor(v => v.Cliente)
                .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .WithMessage("Uma venda só pode ser realizada mediante a informação de um cliente cadastrado.");

            RuleFor(v => v.Produtos)
                .Custom((produtos, context) =>
                {
                    if (produtos.Count == 0)
                        context.AddFailure("Produtos", "A venda deve conter ao menos um produto.");

                    produtos.Select(p => p.Quantidade).ToList().ForEach(quantidade =>
                    {
                        if (quantidade <= 0)
                            context.AddFailure("Quantidade", "A quantidade de um produto deve ser maior que zero.");
                    });

                    produtos.Select(p => p.PrecoUnitario).ToList().ForEach(valorUnitario =>
                    {
                        if (valorUnitario <= 0)
                            context.AddFailure("ValorUnitario", "O valor unitário de um produto deve ser maior que zero.");
                    });

                    if (!produtos.Sum(x => x.PrecoUnitario * x.Quantidade)
                        .Equals(context.InstanceToValidate.SubTotal))
                            context.AddFailure("SubTotal", 
                                "O valor do SubTotal deve ser a soma dos valores unitários multiplicados pelas quantidades dos produtos.");

                    if (context.InstanceToValidate.SubTotal 
                        - context.InstanceToValidate.Cliente.CategoriaCliente.Desconto
                        != context.InstanceToValidate.Total)
                            context.AddFailure("ValorTotal",
                            "O valor total da venda deve ser o SubTotal menos o desconto do cliente.");
                });

            RuleFor(v => v.StatusPedido)
                .Must(
                    status => status.Equals(StatusPedido.Invalido) || 
                    status.Equals(StatusPedido.Criado) ||
                    status.Equals(StatusPedido.Pago) ||
                    status.Equals(StatusPedido.EmAndamento) || 
                    status.Equals(StatusPedido.Concluido))
                        .WithMessage(
                            "O status válidos para o pedido devem ser 'Invalido', 'Criado', 'Pago', 'EmAndamento' ou 'Concluido'.");
        }
    }
}
