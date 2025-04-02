using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Clientes;
using Domain.Models.Clientes;

namespace Domain.Services.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(Guid id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            await _clienteRepository.AddAsync(cliente);
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteClienteAsync(Guid id)
        {
            await _clienteRepository.DeleteAsync(id);
        }

        public async Task<Cliente> GetClienteByNomeAsync(string nome)
        {
            return await _clienteRepository.GetClienteByNomeAsync(nome);
        }

        public async Task<Cliente> GetClienteByCpfAsync(string cpf)
        {
            return await _clienteRepository.GetClienteByCpfAsync(cpf);
        }

        public async Task<Cliente> GetClienteByEmailAsync(string email)
        {
            return await _clienteRepository.GetClienteByEmailAsync(email);
        }

        public async Task<Cliente> GetClienteByTelefoneAsync(string telefone)
        {
            return await _clienteRepository.GetClienteByTelefoneAsync(telefone);
        }

        public async Task<IEnumerable<Cliente>> GetClientesByCategoriaClienteIdAsync(Guid categoriaClienteId)
        {
            return await _clienteRepository.GetClientesByCategoriaClienteIdAsync(categoriaClienteId);
        }

    }
}
