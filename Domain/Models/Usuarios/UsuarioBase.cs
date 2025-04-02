using Microsoft.AspNetCore.Identity;
using Domain.Models.Base;

namespace Domain.Models.Usuarios
{
    public class UsuarioBase : IdentityUser
    {
        public EntidadePessoaSistema RegistroEntidadePessoaSistema { get; set; }
        public string TipoUsuario { get; set; }

        public UsuarioBase()
        {
        }

        public UsuarioBase(EntidadePessoaSistema registroEntidadePessoaSistema, string tipoUsuario)
        {
            RegistroEntidadePessoaSistema = registroEntidadePessoaSistema;
            TipoUsuario = tipoUsuario;
        }
    }
} 