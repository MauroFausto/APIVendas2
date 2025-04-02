using Domain.Interfaces.Repositories;
using Domain.Models.Vendas;
using Microsoft.EntityFrameworkCore;
using Data.Context;

namespace Data.Repositories.Vendas
{
    public class VendaRepository : GenericRepository<Venda>, IVendaRepository
    {
        public VendaRepository(VendasDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Venda>> GetAllVendasAsync()
        {
            return await _context.Set<Venda>().ToListAsync();
        }

        public async Task<Venda> GetVendaByIdAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Set<Venda>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task AddVendaAsync(Venda venda)
        {
            await _context.Set<Venda>().AddAsync(venda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVendaAsync(Venda venda)
        {
            _context.Set<Venda>().Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVendaAsync(Guid id)
        {
            var venda = await GetVendaByIdAsync(id);
            if (venda != null)
            {
                _context.Set<Venda>().Remove(venda);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Venda>> GetVendasByClienteIdAsync(Guid clienteId)
        {
            return await _context.Set<Venda>().Where(v => v.Cliente.Id == clienteId).ToListAsync();
        }
    }
}
