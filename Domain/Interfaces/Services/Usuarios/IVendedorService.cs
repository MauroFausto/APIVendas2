using Domain.Models.Usuarios;

namespace Domain.Interfaces.Services.Usuarios
{
    public interface IVendedorService
    {
        Task<IEnumerable<Vendedor>> GetAllVendedoresAsync();
        Task<Vendedor> GetVendedorByIdAsync(Guid id);
        Task AddVendedorAsync(Vendedor vendedor);
        Task UpdateVendedorAsync(Vendedor vendedor);
        Task DeleteVendedorAsync(Guid id);
        Task<Vendedor> GetVendedorByEmailAsync(string email);
        Task<Vendedor> GetVendedorByCpfAsync(string cpf);
        Task<Vendedor> GetVendedorByEmailAndSenhaAsync(string email, string senha);
        Task<Vendedor> GetVendedorByCpfAndSenhaAsync(string cpf, string senha);
    }
}
