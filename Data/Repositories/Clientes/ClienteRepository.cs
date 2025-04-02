using Domain.Interfaces.Repositories;
using Domain.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using Data.Context;

namespace Data.Repositories.Clientes
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository 
    {
        public ClienteRepository(VendasDbContext context) : base(context)
        {
        }

        public async Task<Cliente> GetClienteByNomeAsync(string nome)
        {
            return await _context.Set<Cliente>().FirstOrDefaultAsync(c => c.Nome.Equals(nome));
        }

        public async Task<Cliente> GetClienteByCpfAsync(string cpf)
        {
            return await _context.Set<Cliente>().FirstOrDefaultAsync(c => c.DocumentoOficial.Equals(cpf));
        }

        public async Task<Cliente> GetClienteByEmailAsync(string email)
        {
            return await _context.Set<Cliente>().FirstOrDefaultAsync(c => c.Email.Equals(email));
        }

        public async Task<Cliente> GetClienteByTelefoneAsync(string telefone)
        {
            return await _context.Set<Cliente>().FirstOrDefaultAsync(c => c.Telefone.Equals(telefone));
        }

        public async Task<IEnumerable<Cliente>> GetClientesByCategoriaClienteIdAsync(Guid categoriaClienteId)
        {
            return await _context.Set<Cliente>().Where(c => c.CategoriaClienteId.Equals(categoriaClienteId)).ToListAsync();
        }

    }
}
