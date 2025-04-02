using Domain.Models.Base;
using Domain.Models.Clientes;

namespace Domain.Models.Vendas
{
    public class Venda : Entidade
    {
        public required Guid ClienteId { get; set; }
        public required Cliente Cliente { get; set; }
        public required List<Produto> Produtos { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public StatusPedido StatusPedido { get; set; }
    }

    public enum StatusPedido
    {
        Invalido = -1,
        Criado = 0,
        Pago = 1,
        EmAndamento = 2,
        Concluido = 3
    }
}
