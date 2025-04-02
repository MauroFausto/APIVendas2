using Domain.Models.Clientes;

namespace Domain.Interfaces.Services.Clientes
{
    public interface ICategoriaClienteService
    {
        Task<IEnumerable<CategoriaCliente>> GetAllCategoriasClienteAsync();
        Task<CategoriaCliente> GetCategoriaClienteByIdAsync(Guid id);
        Task AddCategoriaClienteAsync(CategoriaCliente categoriaCliente);
        Task UpdateCategoriaClienteAsync(CategoriaCliente categoriaCliente);
        Task DeleteCategoriaClienteAsync(Guid id);
        Task<IEnumerable<CategoriaCliente>> GetCategoriasClienteByNomeAsync(string nome);
        Task<IEnumerable<CategoriaCliente>> GetCategoriasClienteByDescricaoAsync(string descricao);
    }
}
