using Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class EntidadeMapping : IEntityTypeConfiguration<Entidade>
    {
        public void Configure(EntityTypeBuilder<Entidade> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Observacoes)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.DataCriacao)
                .IsRequired();

            builder.Property(e => e.IdUsuarioCriacao)
                .IsRequired();

            builder.Property(e => e.DataAtualizacao)
                .IsRequired();

            builder.Property(e => e.IdUsuarioAtualizacao)
                .IsRequired();
        }
    }
} 