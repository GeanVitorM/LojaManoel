using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaManoel.API.Models;
using LojaManoel.API.Services;
using LojaManoel.API.Controllers;
using LojaManoel.API.DTOs;
using LojaManoel.API.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LojaManoel.Tests.Controllers
{
    public class PedidosControllerTests
    {
        private readonly Mock<IPedidoService> _mockPedidoService;
        private readonly PedidosController _controller;

        public PedidosControllerTests()
        {
            _mockPedidoService = new Mock<IPedidoService>();
            _controller = new PedidosController(_mockPedidoService.Object);
        }

        [Fact]
        public async Task ObterTodos_ComPedidos_DeveRetornarOk()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, PedidoId = 1 },
                new Pedido { Id = 2, PedidoId = 2 }
            };

            _mockPedidoService.Setup(x => x.ObterTodos())
                .ReturnsAsync(pedidos);

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Pedido>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task ObterPorId_PedidoExistente_DeveRetornarOk()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, PedidoId = 1 };
            _mockPedidoService.Setup(x => x.ObterPorId(1))
                .ReturnsAsync(pedido);

            // Act
            var result = await _controller.ObterPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Pedido>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task ObterPorId_PedidoInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _mockPedidoService.Setup(x => x.ObterPorId(1))
                .ReturnsAsync((Pedido?)null);

            // Act
            var result = await _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Criar_PedidoValido_DeveRetornarCreated()
        {
            // Arrange
            var pedido = new Pedido { PedidoId = 1 };
            var pedidoCriado = new Pedido { Id = 1, PedidoId = 1 };

            _mockPedidoService.Setup(x => x.Criar(pedido))
                .ReturnsAsync(pedidoCriado);

            // Act
            var result = await _controller.Criar(pedido);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Pedido>(createdResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Atualizar_PedidoExistente_DeveRetornarOk()
        {
            // Arrange
            var pedido = new Pedido { PedidoId = 1 };
            var pedidoAtualizado = new Pedido { Id = 1, PedidoId = 1 };

            _mockPedidoService.Setup(x => x.Atualizar(1, pedido))
                .ReturnsAsync(pedidoAtualizado);

            // Act
            var result = await _controller.Atualizar(1, pedido);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Pedido>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Atualizar_PedidoInexistente_DeveRetornarNotFound()
        {
            // Arrange
            var pedido = new Pedido { PedidoId = 1 };
            _mockPedidoService.Setup(x => x.Atualizar(1, pedido))
                .ReturnsAsync((Pedido?)null);

            // Act
            var result = await _controller.Atualizar(1, pedido);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Deletar_PedidoExistente_DeveRetornarNoContent()
        {
            // Arrange
            _mockPedidoService.Setup(x => x.Deletar(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Deletar(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Deletar_PedidoInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _mockPedidoService.Setup(x => x.Deletar(1))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Deletar(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
