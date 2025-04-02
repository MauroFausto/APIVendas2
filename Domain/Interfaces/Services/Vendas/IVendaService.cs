using Domain.Models.Clientes;
using Domain.Models.Vendas;

namespace Domain.Interfaces.Services.Vendas
{
    public interface IVendaService
    {
        Task<IEnumerable<Venda>> GetAllVendasAsync();
        Task<Venda> GetVendaByIdAsync(Guid id);
        Task AddVendaAsync(Venda venda);
        Task UpdateVendaAsync(Venda venda);
        Task DeleteVendaAsync(Guid id);
    }
}
