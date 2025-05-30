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

namespace LojaManoel.Tests.Services
{
    public class EmbalagemServiceTests
    {
        private readonly Mock<ICaixaService> _mockCaixaService;
        private readonly EmbalagemService _embalagemService;

        public EmbalagemServiceTests()
        {
            _mockCaixaService = new Mock<ICaixaService>();
            _embalagemService = new EmbalagemService(_mockCaixaService.Object);
        }

        [Fact]
        public async Task ProcessarEmbalagem_PedidoVazio_DeveRetornarListaVazia()
        {
            // Arrange
            var input = new PedidoInputDto { Pedidos = new List<PedidoDto>() };

            // Act
            var resultado = await _embalagemService.ProcessarEmbalagem(input);

            // Assert
            Assert.Empty(resultado.Pedidos);
        }

        [Fact]
        public async Task ProcessarEmbalagem_ProdutoQueNaoCabe_DeveRetornarObservacao()
        {
            // Arrange
            var input = new PedidoInputDto
            {
                Pedidos = new List<PedidoDto>
                {
                    new PedidoDto
                    {
                        PedidoId = 5,
                        Produtos = new List<ProdutoDto>
                        {
                            new ProdutoDto
                            {
                                ProdutoId = "Cadeira Gamer",
                                Dimensoes = new DimensoesDto { Altura = 120, Largura = 60, Comprimento = 70 }
                            }
                        }
                    }
                }
            };

            _mockCaixaService.Setup(x => x.EncontrarCaixaCompativel(It.IsAny<Produto>(), It.IsAny<List<Produto>>()))
                .Returns((Caixa?)null);

            // Act
            var resultado = await _embalagemService.ProcessarEmbalagem(input);

            // Assert
            Assert.Single(resultado.Pedidos);
            var pedido = resultado.Pedidos.First();
            Assert.Equal(5, pedido.PedidoId);
            Assert.Single(pedido.Caixas);
            var caixa = pedido.Caixas.First();
            Assert.Null(caixa.CaixaId);
            Assert.Equal("Produto não cabe em nenhuma caixa disponível.", caixa.Observacao);
        }

        [Fact]
        public async Task ProcessarEmbalagem_ProdutoSimples_DeveFuncionar()
        {
            // Arrange
            var input = new PedidoInputDto
            {
                Pedidos = new List<PedidoDto>
                {
                    new PedidoDto
                    {
                        PedidoId = 1,
                        Produtos = new List<ProdutoDto>
                        {
                            new ProdutoDto
                            {
                                ProdutoId = "Headset",
                                Dimensoes = new DimensoesDto { Altura = 25, Largura = 15, Comprimento = 20 }
                            }
                        }
                    }
                }
            };

            var caixaMock = new Caixa { Id = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 };
            _mockCaixaService.Setup(x => x.EncontrarCaixaCompativel(It.IsAny<Produto>(), It.IsAny<List<Produto>>()))
                .Returns(caixaMock);

            // Act
            var resultado = await _embalagemService.ProcessarEmbalagem(input);

            // Assert
            Assert.Single(resultado.Pedidos);
            var pedido = resultado.Pedidos.First();
            Assert.Equal(1, pedido.PedidoId);
            Assert.Single(pedido.Caixas);
            var caixa = pedido.Caixas.First();
            Assert.Equal("Caixa 1", caixa.CaixaId);
            Assert.Contains("Headset", caixa.Produtos);
        }
    }
}
