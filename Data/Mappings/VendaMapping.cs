using Domain.Models.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class VendaMapping : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.Property(e => e.SubTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(e => e.Total)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(e => e.StatusPedido)
                .IsRequired();

            builder.HasOne(e => e.Cliente)
                .WithMany()
                .HasForeignKey(e => e.ClienteId)
                .IsRequired();

            builder.HasMany(e => e.Produtos)
                .WithMany();
        }
    }
} 