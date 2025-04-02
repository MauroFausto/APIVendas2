using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Clientes;
using Domain.Models.Clientes;

namespace Domain.Services.Clientes
{
    public class CategoriaClienteService : ICategoriaClienteService
    {
        private readonly ICategoriaClienteRepository _categoriaClienteRepository;

        public CategoriaClienteService(ICategoriaClienteRepository categoriaClienteRepository)
        {
            _categoriaClienteRepository = categoriaClienteRepository;
        }

        public async Task<IEnumerable<CategoriaCliente>> GetAllCategoriasClienteAsync()
        {
            return await _categoriaClienteRepository.GetAllAsync();
        }

        public async Task<CategoriaCliente> GetCategoriaClienteByIdAsync(Guid id)
        {
            return await _categoriaClienteRepository.GetByIdAsync(id);
        }

        public async Task AddCategoriaClienteAsync(CategoriaCliente categoriaCliente)
        {
            await _categoriaClienteRepository.AddAsync(categoriaCliente);
        }

        public async Task UpdateCategoriaClienteAsync(CategoriaCliente categoriaCliente)
        {
            await _categoriaClienteRepository.UpdateAsync(categoriaCliente);
        }

        public async Task DeleteCategoriaClienteAsync(Guid id)
        {
            await _categoriaClienteRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoriaCliente?>> GetCategoriasClienteByNomeAsync(string nome)
        {
            return await _categoriaClienteRepository.GetCategoriasClienteByNomeAsync(nome);
        }

        public async Task<IEnumerable<CategoriaCliente?>> GetCategoriasClienteByDescricaoAsync(string descricao)
        {
            return await _categoriaClienteRepository.GetCategoriasClienteByDescricaoAsync(descricao);
        }
    }
}
