using System.Net.Http.Json;
using Xunit;
using LojaManoel.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaManoel.Tests.Integration
{
    public class EmpacotamentoIntegrationTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public EmpacotamentoIntegrationTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task CalcularMelhorCaixa_ProdutoValido_RetornaSucesso()
        {
            // Arrange
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    new Produto
                    {
                        Nome = "Produto Teste",
                        Dimensoes = new Dimensoes { Altura = 20, Largura = 30, Comprimento = 40 }
                    }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/empacotamento/calcular", pedido);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultado = await response.Content.ReadFromJsonAsync<ResultadoEmpacotamento>();
            Assert.NotNull(resultado);
            Assert.NotNull(resultado.CaixaSelecionada);
        }

        [Fact]
        public async Task CalcularMelhorCaixa_ProdutoMuitoGrande_RetornaNotFound()
        {
            // Arrange
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    new Produto
                    {
                        Nome = "Produto Muito Grande",
                        Dimensoes = new Dimensoes { Altura = 1000, Largura = 1000, Comprimento = 1000 }
                    }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/empacotamento/calcular", pedido);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
} 