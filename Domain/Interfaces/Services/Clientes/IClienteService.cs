using Domain.Models.Clientes;

namespace Domain.Interfaces.Services.Clientes
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(Guid id);
        Task AddClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(Guid id);
        Task<Cliente> GetClienteByNomeAsync(string nome);
        Task<Cliente> GetClienteByCpfAsync(string cpf);
        Task<Cliente> GetClienteByEmailAsync(string email);
        Task<Cliente> GetClienteByTelefoneAsync(string telefone);
        Task<IEnumerable<Cliente>> GetClientesByCategoriaClienteIdAsync(Guid categoriaId);
    }
}
