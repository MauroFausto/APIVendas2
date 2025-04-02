using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Domain.Models.Clientes;
using Domain.Models.Usuarios;
using Domain.Models.Vendas;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data.Context
{
    public class VendasDbContext : IdentityDbContext<UsuarioBase>
    {
        public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options)
        {
        }

        public DbSet<CategoriaCliente> CategoriasCliente { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure inheritance for user types
            modelBuilder.Entity<UsuarioBase>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<UsuarioBase>("UsuarioBase")
                .HasValue<Admin>("Admin")
                .HasValue<Vendedor>("Vendedor");

            // Configure RegistroEntidadePessoaSistema relationship for UsuarioBase
            modelBuilder.Entity<UsuarioBase>(entity =>
            {
                entity.HasOne(e => e.RegistroEntidadePessoaSistema)
                    .WithOne()
                    .HasForeignKey<UsuarioBase>("RegistroEntidadePessoaSistemaId")
                    .IsRequired();
            });

            // Configure Admin entity
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasBaseType<UsuarioBase>();
                entity.Property(e => e.TipoUsuario)
                    .HasDefaultValue("Admin")
                    .HasMaxLength(50);
            });

            // Configure Vendedor entity
            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasBaseType<UsuarioBase>();
                entity.Property(e => e.TipoUsuario)
                    .HasDefaultValue("Vendedor")
                    .HasMaxLength(50);
                entity.Property(e => e.CodigoVendedor)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }

    // Design-time factory for EF Core migrations
    public class VendasDbContextFactory : IDesignTimeDbContextFactory<VendasDbContext>
    {
        public VendasDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VendasDbContext>();
            
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=VendasDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            
            optionsBuilder.UseSqlServer(connectionString);
            
            return new VendasDbContext(optionsBuilder.Options);
        }
    }
}
