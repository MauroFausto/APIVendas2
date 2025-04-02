using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.Vendas;
using Domain.Models.Vendas;
using Domain.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Vendas.API.Domain.Services.Vendas
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        public VendaService(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public async Task<IEnumerable<Venda>> GetAllVendasAsync()
        {
            return await _vendaRepository.GetAllAsync();
        }

        public async Task<Venda> GetVendaByIdAsync(Guid id)
        {
            return await _vendaRepository.GetByIdAsync(id);
        }

        public async Task AddVendaAsync(Venda venda)
        {
            decimal desconto = venda.Cliente.CategoriaCliente.Desconto;
            List<Produto> copyProdutos = venda.Produtos;

            venda.Cliente = null;
            venda.Produtos = null;

            try
            {
                await _vendaRepository.AddAsync(venda);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            

            using (var httpClient = new HttpClient())
            {
                string apiUrl = "https://sti3-faturamento.azurewebsites.net/api/vendas";
                var requestBody = new
                {
                    identificador = venda.Id.ToString(),
                    subTotal = venda.SubTotal,
                    descontos = desconto,
                    valorTotal = venda.Total,
                    itens = copyProdutos.Select(p => new
                    {
                        quantidade = p.Quantidade,
                        precoUnitario = p.PrecoUnitario
                    }).ToList()
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("Email", "mfmaurofausto5@gmail.com");

                var response = await httpClient.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error calling external API: {errorMessage}");
                }
            }
        }

        public async Task UpdateVendaAsync(Venda venda)
        {
            await _vendaRepository.UpdateAsync(venda);
        }

        public async Task DeleteVendaAsync(Guid id)
        {
            await _vendaRepository.DeleteAsync(id);
        }
    }
}
