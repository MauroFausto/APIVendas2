using Domain.Models.Clientes;
using Domain.Models.Usuarios;
using Domain.Models.Vendas;
using Domain.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Context;

namespace Data.Seeding
{
    public class DatabaseSeeder
    {
        private readonly VendasDbContext _context;
        private readonly UserManager<UsuarioBase> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(VendasDbContext context, UserManager<UsuarioBase> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();

            // Seed Admins
            await SeedAdminsAsync();

            // Seed Vendedores
            await SeedVendedoresAsync();

            // Seed CategoriasCliente
            await SeedCategoriasClienteAsync();

            // Seed Produtos
            await SeedProdutosAsync();

            // Seed Vendas
            await SeedVendasAsync();
        }

        private async Task SeedAdminsAsync()
        {
            if (await _context.Set<Admin>().AnyAsync()) return;

            var admins = new List<Admin>
            {
                new Admin
                {
                    UserName = "admin1@example.com",
                    Email = "admin1@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "11999999999",
                    PhoneNumberConfirmed = true,
                    TipoUsuario = "Admin",
                    RegistroEntidadePessoaSistema = new EntidadePessoaSistema
                    {
                        Nome = "Administrador 1",
                        DocumentoOficial = "12345678900",
                        Telefone = "11999999999",
                        Email = "admin1@example.com"
                    }
                },
                new Admin
                {
                    UserName = "admin2@example.com",
                    Email = "admin2@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "11988888888",
                    PhoneNumberConfirmed = true,
                    TipoUsuario = "Admin",
                    RegistroEntidadePessoaSistema = new EntidadePessoaSistema
                    {
                        Nome = "Administrador 2",
                        DocumentoOficial = "98765432100",
                        Telefone = "11988888888",
                        Email = "admin2@example.com"
                    }
                }
            };

            foreach (var admin in admins)
            {
                var result = await _userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }

        private async Task SeedVendedoresAsync()
        {
            if (await _context.Set<Vendedor>().AnyAsync()) return;

            var vendedores = new List<Vendedor>
            {
                new Vendedor
                {
                    UserName = "vendedor1@example.com",
                    Email = "vendedor1@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "11977777777",
                    PhoneNumberConfirmed = true,
                    TipoUsuario = "Vendedor",
                    PasswordHash = "Vendedor@123",
                    CodigoVendedor = "V001",
                    RegistroEntidadePessoaSistema = new EntidadePessoaSistema
                    {
                        Nome = "Vendedor 1",
                        DocumentoOficial = "11122233344",
                        Telefone = "11977777777",
                        Email = "vendedor1@example.com"
                    }
                },
                new Vendedor
                {
                    UserName = "vendedor2@example.com",
                    Email = "vendedor2@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "11966666666",
                    PhoneNumberConfirmed = true,
                    TipoUsuario = "Vendedor",
                    PasswordHash = "Vendedor@123",
                    CodigoVendedor = "V002",
                    RegistroEntidadePessoaSistema = new EntidadePessoaSistema
                    {
                        Nome = "Vendedor 2",
                        DocumentoOficial = "22233344455",
                        Telefone = "11966666666",
                        Email = "vendedor2@example.com"
                    }
                },
                new Vendedor
                {
                    UserName = "vendedor3@example.com",
                    Email = "vendedor3@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "11955555555",
                    PhoneNumberConfirmed = true,
                    TipoUsuario = "Vendedor",
                    PasswordHash = "Vendedor@123",
                    CodigoVendedor = "V003",
                    RegistroEntidadePessoaSistema = new EntidadePessoaSistema
                    {
                        Nome = "Vendedor 3",
                        DocumentoOficial = "33344455566",
                        Telefone = "11955555555",
                        Email = "vendedor3@example.com"
                    }
                },
                new Vendedor
                {
                    UserName = "vendedor4@example.com",
                    Email = "vendedor4@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "11944444444",
                    PhoneNumberConfirmed = true,
                    TipoUsuario = "Vendedor",
                    PasswordHash = "Vendedor@123",
                    CodigoVendedor = "V004",
                    RegistroEntidadePessoaSistema = new EntidadePessoaSistema
                    {
                        Nome = "Vendedor 4",
                        DocumentoOficial = "44455566677",
                        Telefone = "11944444444",
                        Email = "vendedor4@example.com"
                    }
                }
            };

            foreach (var vendedor in vendedores)
            {
                var result = await _userManager.CreateAsync(vendedor, vendedor.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(vendedor, "Vendedor");
                }
            }
        }

        private async Task SeedCategoriasClienteAsync()
        {
            if (await _context.Set<CategoriaCliente>().AnyAsync()) return;

            var categorias = new List<CategoriaCliente>
            {
                new CategoriaCliente { Categoria = "VIP", Desconto = 15 },
                new CategoriaCliente { Categoria = "Premium", Desconto = 10 },
                new CategoriaCliente { Categoria = "Regular", Desconto = 5 }
            };

            await _context.Set<CategoriaCliente>().AddRangeAsync(categorias);
            await _context.SaveChangesAsync();
        }

        private async Task SeedProdutosAsync()
        {
            if (await _context.Set<Produto>().AnyAsync()) return;

            var produtos = new List<Produto>();
            for (int i = 1; i <= 20; i++)
            {
                produtos.Add(new Produto
                {
                    Descricao = $"Descrição do Produto {i}",
                    Quantidade = 100,
                    PrecoUnitario = 10.0m * i
                });
            }

            await _context.Set<Produto>().AddRangeAsync(produtos);
            await _context.SaveChangesAsync();
        }

        private async Task SeedVendasAsync()
        {
            if (await _context.Set<Venda>().AnyAsync()) return;

            var produtos = await _context.Set<Produto>().ToListAsync();
            var categorias = await _context.Set<CategoriaCliente>().ToListAsync();

            var vendas = new List<Venda>();
            for (int i = 1; i <= 5; i++)
            {
                var categoria = categorias[i % categorias.Count];
                var cliente = new Cliente
                {
                    Nome = $"Cliente {i}",
                    DocumentoOficial = $"1112223334{i}",
                    Telefone = $"1199999999{i}",
                    Email = $"cliente{i}@example.com",
                    CategoriaClienteId = categoria.Id,
                    CategoriaCliente = categoria
                };

                var venda = new Venda
                {
                    ClienteId = cliente.Id,
                    Cliente = cliente,
                    Produtos = new List<Produto>(),
                    SubTotal = 0,
                    Total = 0,
                    StatusPedido = StatusPedido.Criado
                };

                // Add 2-4 items to each sale
                var numItems = new Random().Next(2, 5);
                for (int j = 0; j < numItems; j++)
                {
                    var produto = produtos[new Random().Next(produtos.Count)];
                    venda.Produtos.Add(produto);
                    venda.SubTotal += produto.PrecoUnitario;
                }

                // Calculate total with discount
                venda.Total = venda.SubTotal * (1 - categoria.Desconto / 100m);
                vendas.Add(venda);
            }

            await _context.Set<Venda>().AddRangeAsync(vendas);
            await _context.SaveChangesAsync();
        }
    }
} 