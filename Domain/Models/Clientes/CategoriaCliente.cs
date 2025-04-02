using Domain.Models.Base;

namespace Domain.Models.Clientes
{
    public class CategoriaCliente : Entidade
    {
        public required string Categoria { get; set; }
        public required decimal Desconto { get; set; }
    }
}
