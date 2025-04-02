using Domain.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Usuarios
{
    public class Admin : UsuarioBase
    {
        public Admin() : base()
        {
            TipoUsuario = "Admin";
        }

        public Admin(EntidadePessoaSistema registroEntidadePessoaSistema) 
            : base(registroEntidadePessoaSistema, "Admin")
        {
        }
    }
}
