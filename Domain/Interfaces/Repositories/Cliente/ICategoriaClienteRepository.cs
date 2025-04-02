using Domain.Models.Clientes;

namespace Domain.Interfaces.Repositories
{
    public interface ICategoriaClienteRepository : IGenericRepository<CategoriaCliente>
    {
        Task<IEnumerable<CategoriaCliente?>> GetCategoriasClienteByNomeAsync(string nome);
        Task<IEnumerable<CategoriaCliente?>> GetCategoriasClienteByDescricaoAsync(string descricao);
    }
}
