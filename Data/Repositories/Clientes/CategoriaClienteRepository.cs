using Domain.Interfaces.Repositories;
using Domain.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using Data.Context;

namespace Data.Repositories.Clientes
{
    public class CategoriaClienteRepository : GenericRepository<CategoriaCliente>, ICategoriaClienteRepository
    {
        public CategoriaClienteRepository(VendasDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<CategoriaCliente>> GetAllCategoriasClientesAsync()
        {
            return await _context.Set<CategoriaCliente>().ToListAsync();
        }
        public async Task<CategoriaCliente> GetCategoriaClienteByIdAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Set<CategoriaCliente>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }
        public async Task AddCategoriaClienteAsync(CategoriaCliente categoriaCliente)
        {
            await _context.Set<CategoriaCliente>().AddAsync(categoriaCliente);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCategoriaClienteAsync(CategoriaCliente categoriaCliente)
        {
            _context.Set<CategoriaCliente>().Update(categoriaCliente);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCategoriaClienteAsync(Guid id)
        {
            var categoriaCliente = await GetCategoriaClienteByIdAsync(id);
            if (categoriaCliente != null)
            {
                _context.Set<CategoriaCliente>().Remove(categoriaCliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CategoriaCliente?>> GetCategoriasClienteByNomeAsync(string nome)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            List<CategoriaCliente?> categoriasCliente = _context.Set<CategoriaCliente>().Where(c => c.Categoria.Equals(nome)).ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            if (categoriasCliente != null && categoriasCliente.Count >= 1)
                return await Task.FromResult(categoriasCliente);

#pragma warning disable CS8603 // Possible null reference return.
            return await Task.FromResult(categoriasCliente);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<CategoriaCliente?>> GetCategoriasClienteByDescricaoAsync(string descricao)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            List<CategoriaCliente?> categoriasCliente = _context.Set<CategoriaCliente>().Where(c => c.Categoria.Equals(descricao)).ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            if (categoriasCliente != null && categoriasCliente.Count >= 1)
                return await Task.FromResult(categoriasCliente);

#pragma warning disable CS8603 // Possible null reference return.
            return await Task.FromResult(categoriasCliente);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
