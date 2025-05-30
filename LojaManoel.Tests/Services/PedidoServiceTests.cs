using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaManoel.Tests.Services
{
    public class PedidoServiceTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly PedidoService _pedidoService;

        public PedidoServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _pedidoService = new PedidoService(_context);
        }

        [Fact]
        public async Task Criar_PedidoValido_DeveAdicionarPedido()
        {
            // Arrange
            var pedido = new Pedido
            {
                PedidoId = 1,
                Produtos = new List<Produto>
                {
                    new Produto { ProdutoId = "PS5", Altura = 40, Largura = 10, Comprimento = 25 }
                }
            };

            // Act
            var resultado = await _pedidoService.Criar(pedido);

            // Assert
            Assert.NotEqual(0, resultado.Id);
            Assert.Equal(1, resultado.PedidoId);
            Assert.Single(resultado.Produtos);
        }

        [Fact]
        public async Task ObterTodos_ComPedidos_DeveRetornarTodos()
        {
            // Arrange
            var pedido1 = new Pedido { PedidoId = 1, Produtos = new List<Produto>() };
            var pedido2 = new Pedido { PedidoId = 2, Produtos = new List<Produto>() };

            await _pedidoService.Criar(pedido1);
            await _pedidoService.Criar(pedido2);

            // Act
            var pedidos = await _pedidoService.ObterTodos();

            // Assert
            Assert.Equal(2, pedidos.Count());
        }

        [Fact]
        public async Task ObterPorId_PedidoExistente_DeveRetornarPedido()
        {
            // Arrange
            var pedido = new Pedido
            {
                PedidoId = 1,
                Produtos = new List<Produto>
                {
                    new Produto { ProdutoId = "PS5", Altura = 40, Largura = 10, Comprimento = 25 }
                }
            };
            var criado = await _pedidoService.Criar(pedido);

            // Act
            var resultado = await _pedidoService.ObterPorId(criado.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.PedidoId);
            Assert.Single(resultado.Produtos);
        }

        [Fact]
        public async Task ObterPorId_PedidoInexistente_DeveRetornarNull()
        {
            // Act
            var resultado = await _pedidoService.ObterPorId(999);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Atualizar_PedidoExistente_DeveAtualizar()
        {
            // Arrange
            var pedido = new Pedido
            {
                PedidoId = 1,
                Produtos = new List<Produto>
                {
                    new Produto { ProdutoId = "PS5", Altura = 40, Largura = 10, Comprimento = 25 }
                }
            };
            var criado = await _pedidoService.Criar(pedido);

            var pedidoAtualizado = new Pedido
            {
                PedidoId = 2,
                Produtos = new List<Produto>
                {
                    new Produto { ProdutoId = "Xbox", Altura = 35, Largura = 15, Comprimento = 30 }
                }
            };

            // Act
            var resultado = await _pedidoService.Atualizar(criado.Id, pedidoAtualizado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.PedidoId);
            Assert.Single(resultado.Produtos);
            Assert.Equal("Xbox", resultado.Produtos.First().ProdutoId);
        }

        [Fact]
        public async Task Deletar_PedidoExistente_DeveRemover()
        {
            // Arrange
            var pedido = new Pedido { PedidoId = 1, Produtos = new List<Produto>() };
            var criado = await _pedidoService.Criar(pedido);

            // Act
            var resultado = await _pedidoService.Deletar(criado.Id);

            // Assert
            Assert.True(resultado);

            var pedidoRemovido = await _pedidoService.ObterPorId(criado.Id);
            Assert.Null(pedidoRemovido);
        }

        [Fact]
        public async Task Deletar_PedidoInexistente_DeveRetornarFalse()
        {
            // Act
            var resultado = await _pedidoService.Deletar(999);

            // Assert
            Assert.False(resultado);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
