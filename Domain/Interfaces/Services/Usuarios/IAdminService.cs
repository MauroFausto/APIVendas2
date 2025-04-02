using Domain.Models.Usuarios;

namespace Domain.Interfaces.Services.Usuarios
{
    public interface IAdminService
    {
        Task<IEnumerable<Admin>> GetAllAdminsAsync();
        Task<Admin> GetAdminByIdAsync(Guid id);
        Task AddAdminAsync(Admin admin);
        Task UpdateAdminAsync(Admin admin);
        Task DeleteAdminAsync(Guid id);
        Task<Admin> GetAdminByEmailAsync(string email);
        Task<Admin> GetAdminByCpfAsync(string cpf);
        Task<Admin> GetAdminByEmailAndSenhaAsync(string email, string senha);
        Task<Admin> GetAdminByCpfAndSenhaAsync(string cpf, string senha);

    }
}
