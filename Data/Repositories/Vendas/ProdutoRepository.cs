using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Vendas;
using Domain.Interfaces.Repositories;
using Data.Context;

namespace Data.Repositories.Vendas
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(VendasDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            return await _context.Set<Produto>().ToListAsync();
        }

        public async Task<Produto> GetProdutoByIdAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Set<Produto>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task AddProdutoAsync(Produto produto)
        {
            await _context.Set<Produto>().AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProdutoAsync(Produto produto)
        {
            _context.Set<Produto>().Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProdutoAsync(Guid id)
        {
            var produto = await GetProdutoByIdAsync(id);
            if (produto != null)
            {
                _context.Set<Produto>().Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
