using Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class EntidadePessoaMapping : IEntityTypeConfiguration<EntidadePessoa>
    {
        public void Configure(EntityTypeBuilder<EntidadePessoa> builder)
        {
            builder.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.DocumentoOficial)
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(e => e.Telefone)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
} 