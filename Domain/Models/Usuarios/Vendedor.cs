using Domain.Models.Base;

namespace Domain.Models.Usuarios
{
    public class Vendedor : UsuarioBase
    {
        public string CodigoVendedor { get; set; }

        public Vendedor() : base()
        {
            TipoUsuario = "Vendedor";
        }

        public Vendedor(EntidadePessoaSistema registroEntidadePessoaSistema, string codigoVendedor) 
            : base(registroEntidadePessoaSistema, "Vendedor")
        {
            CodigoVendedor = codigoVendedor;
        }
    }
}
