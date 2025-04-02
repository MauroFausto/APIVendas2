using Domain.Models.Clientes;

namespace Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<Cliente> GetClienteByNomeAsync(string nome);
        Task<Cliente> GetClienteByCpfAsync(string cpf);
        Task<Cliente> GetClienteByEmailAsync(string email);
        Task<Cliente> GetClienteByTelefoneAsync(string telefone);
        Task<IEnumerable<Cliente>> GetClientesByCategoriaClienteIdAsync(Guid categoriaClienteId);
    }
}
