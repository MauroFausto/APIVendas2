using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Vendas;
using Domain.Models.Vendas;

namespace Vendas.API.Domain.Services.Vendas
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto> GetProdutoByIdAsync(Guid id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task AddProdutoAsync(Produto produto)
        {
            await _produtoRepository.AddAsync(produto);
        }

        public async Task UpdateProdutoAsync(Produto produto)
        {
            await _produtoRepository.UpdateAsync(produto);
        }

        public async Task DeleteProdutoAsync(Guid id)
        {
            await _produtoRepository.DeleteAsync(id);
        }
    }
}
