using Domain.Models.Usuarios;


namespace Domain.Interfaces.Repositories
{
    public interface IVendedorRepository : IGenericRepository<Vendedor>
    {
        Task<Vendedor?> GetByEmailAsync(string email);
        Task<Vendedor?> GetByCpfAsync(string cpf);
        Task<Vendedor?> GetByEmailAndSenhaAsync(string email, string senha);
        Task<Vendedor?> GetByCpfAndSenhaAsync(string cpf, string senha);
    }
}
