namespace Domain.Models.Base
{
    public abstract class EntidadePessoa : Entidade
    {
        public required string Nome { get; set; }
        public required string DocumentoOficial { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
    }
}
