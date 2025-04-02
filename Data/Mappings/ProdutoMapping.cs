using Domain.Models.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(e => e.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Quantidade)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(e => e.PrecoUnitario)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
} 