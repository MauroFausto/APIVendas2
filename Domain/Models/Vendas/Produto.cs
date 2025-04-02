using Domain.Models.Base;

namespace Domain.Models.Vendas
{
    public class Produto : Entidade
    {
        public required string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
