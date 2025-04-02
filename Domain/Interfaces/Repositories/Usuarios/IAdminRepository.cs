using Domain.Models.Usuarios;

namespace Domain.Interfaces.Repositories
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin?> GetByEmailAsync(string email);
        Task<Admin?> GetByCpfAsync(string cpf);
        Task<Admin?> GetByEmailAndSenhaAsync(string email, string senha);
        Task<Admin?> GetByCpfAndSenhaAsync(string cpf, string senha);
    }
}
