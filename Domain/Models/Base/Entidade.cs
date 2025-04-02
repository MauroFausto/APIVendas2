namespace Domain.Models.Base
{
    public abstract class Entidade
    {
        public Guid Id { get; set; }
        public string Observacoes { get; set; } = "";
        public DateTime DataCriacao { get; set; }
        public Guid IdUsuarioCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public Guid IdUsuarioAtualizacao { get; set; }
    }
}
