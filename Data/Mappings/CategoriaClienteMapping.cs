using Domain.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class CategoriaClienteMapping : IEntityTypeConfiguration<CategoriaCliente>
    {
        public void Configure(EntityTypeBuilder<CategoriaCliente> builder)
        {
            builder.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Desconto)
                .HasPrecision(5, 2)
                .IsRequired();
        }
    }
} 