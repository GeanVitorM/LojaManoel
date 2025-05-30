using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaManoel.Tests.Controllers
{
    public class EmbalagemControllerTests
    {
        private readonly Mock<IEmbalagemService> _mockEmbalagemService;
        private readonly EmbalagemController _controller;

        public EmbalagemControllerTests()
        {
            _mockEmbalagemService = new Mock<IEmbalagemService>();
            _controller = new EmbalagemController(_mockEmbalagemService.Object);
        }

        [Fact]
        public async Task ProcessarEmbalagem_InputValido_DeveRetornarOk()
        {
            // Arrange
            var input = new PedidoInputDto
            {
                Pedidos = new List<PedidoDto>
                {
                    new PedidoDto { PedidoId = 1, Produtos = new List<ProdutoDto>() }
                }
            };

            var output = new EmbalagemOutputDto
            {
                Pedidos = new List<PedidoEmbalagemDto>
                {
                    new PedidoEmbalagemDto { PedidoId = 1, Caixas = new List<CaixaEmbalagemDto>() }
                }
            };

            _mockEmbalagemService.Setup(x => x.ProcessarEmbalagem(input))
                .ReturnsAsync(output);

            // Act
            var result = await _controller.ProcessarEmbalagem(input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EmbalagemOutputDto>(okResult.Value);
            Assert.Single(returnValue.Pedidos);
        }

        [Fact]
        public async Task ProcessarEmbalagem_InputNull_DeveRetornarBadRequest()
        {
            // Act
            var result = await _controller.ProcessarEmbalagem(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task ProcessarEmbalagem_PedidosVazio_DeveRetornarBadRequest()
        {
            // Arrange
            var input = new PedidoInputDto { Pedidos = new List<PedidoDto>() };

            // Act
            var result = await _controller.ProcessarEmbalagem(input);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
