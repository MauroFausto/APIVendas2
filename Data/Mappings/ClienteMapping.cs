using Domain.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasOne(e => e.CategoriaCliente)
                .WithMany()
                .HasForeignKey(e => e.CategoriaClienteId)
                .IsRequired();
        }
    }
} 