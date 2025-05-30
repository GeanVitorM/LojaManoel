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

namespace LojaManoel.Tests.Services;

public class CaixaServiceTests
{
    private readonly CaixaService _caixaService;

    public CaixaServiceTests()
    {
        _caixaService = new CaixaService();
    }

    [Fact]
    public void ObterCaixasDisponiveis_DeveRetornar3Caixas()
    {
        // Act
        var caixas = _caixaService.ObterCaixasDisponiveis();

        // Assert
        Assert.Equal(3, caixas.Count);
        Assert.Contains(caixas, c => c.Id == "Caixa 1");
        Assert.Contains(caixas, c => c.Id == "Caixa 2");
        Assert.Contains(caixas, c => c.Id == "Caixa 3");
    }

    [Fact]
    public void EncontrarCaixaCompativel_ProdutoPequeno_DeveRetornarCaixa1()
    {
        // Arrange
        var produto = new Produto
        {
            ProdutoId = "Teste",
            Altura = 10,
            Largura = 10,
            Comprimento = 10
        };

        // Act
        var caixa = _caixaService.EncontrarCaixaCompativel(produto, new List<Produto>());

        // Assert
        Assert.NotNull(caixa);
        Assert.Equal("Caixa 1", caixa.Id);
    }

    [Fact]
    public void EncontrarCaixaCompativel_ProdutoMuitoGrande_DeveRetornarNull()
    {
        // Arrange
        var produto = new Produto
        {
            ProdutoId = "Cadeira Gamer",
            Altura = 120,
            Largura = 60,
            Comprimento = 70
        };

        // Act
        var caixa = _caixaService.EncontrarCaixaCompativel(produto, new List<Produto>());

        // Assert
        Assert.Null(caixa);
    }

    [Theory]
    [InlineData(40, 10, 25, "Caixa 2")] 
    [InlineData(40, 30, 30, "Caixa 2")] 
    [InlineData(25, 15, 20, "Caixa 1")] 
    public void EncontrarCaixaCompativel_ProdutosReais_DeveRetornarCaixaCorreta(
        int altura, int largura, int comprimento, string caixaEsperada)
    {
        // Arrange
        var produto = new Produto
        {
            ProdutoId = "Teste",
            Altura = altura,
            Largura = largura,
            Comprimento = comprimento
        };

        // Act
        var caixa = _caixaService.EncontrarCaixaCompativel(produto, new List<Produto>());

        // Assert
        Assert.NotNull(caixa);
        Assert.Equal(caixaEsperada, caixa.Id);
    }
}
