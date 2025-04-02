using Domain.Models.Base;

namespace Domain.Models.Clientes
{
    public class Cliente : EntidadePessoa
    {
        public required Guid CategoriaClienteId { get; set; }
        public required CategoriaCliente CategoriaCliente { get; set; }
    }
}
