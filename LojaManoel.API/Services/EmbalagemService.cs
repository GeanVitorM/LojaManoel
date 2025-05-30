using LojaManoel.API.DTOs;
using LojaManoel.API.Models;

namespace LojaManoel.API.Services;

public class EmbalagemService : IEmbalagemService
{
    private readonly ICaixaService _caixaService;

    public EmbalagemService(ICaixaService caixaService)
    {
        _caixaService = caixaService;
    }

    public async Task<EmbalagemOutputDto> ProcessarEmbalagem(PedidoInputDto input)
    {
        var resultado = new EmbalagemOutputDto();

        foreach (var pedidoDto in input.Pedidos)
        {
            var produtos = pedidoDto.Produtos.Select(p => new Produto
            {
                ProdutoId = p.ProdutoId,
                Altura = p.Dimensoes.Altura,
                Largura = p.Dimensoes.Largura,
                Comprimento = p.Dimensoes.Comprimento
            }).ToList();

            var caixas = ProcessarProdutos(produtos);

            resultado.Pedidos.Add(new PedidoEmbalagemDto
            {
                PedidoId = pedidoDto.PedidoId,
                Caixas = caixas
            });
        }

        return resultado;
    }

    private List<CaixaEmbalagemDto> ProcessarProdutos(List<Produto> produtos)
    {
        var caixas = new List<CaixaEmbalagemDto>();
        var produtosRestantes = produtos.ToList();

        while (produtosRestantes.Any())
        {
            var caixaAtual = new List<Produto>();
            var produtosParaRemover = new List<Produto>();

            foreach (var produto in produtosRestantes.OrderByDescending(p => p.Volume))
            {
                var caixaCompativel = _caixaService.EncontrarCaixaCompativel(produto, caixaAtual);

                if (caixaCompativel != null)
                {
                    caixaAtual.Add(produto);
                    produtosParaRemover.Add(produto);

                    if (caixas.Count == 0 || caixas.Last().CaixaId != caixaCompativel.Id)
                    {
                        caixas.Add(new CaixaEmbalagemDto
                        {
                            CaixaId = caixaCompativel.Id,
                            Produtos = new List<string>()
                        });
                    }
                }
            }

            if (!produtosParaRemover.Any())
            {
                // Produto não cabe em nenhuma caixa
                var produtoProblematico = produtosRestantes.First();
                caixas.Add(new CaixaEmbalagemDto
                {
                    CaixaId = null,
                    Produtos = new List<string> { produtoProblematico.ProdutoId },
                    Observacao = "Produto não cabe em nenhuma caixa disponível."
                });
                produtosRestantes.Remove(produtoProblematico);
            }
            else
            {
                // Adiciona produtos à última caixa
                var ultimaCaixa = caixas.Last();
                ultimaCaixa.Produtos.AddRange(produtosParaRemover.Select(p => p.ProdutoId));

                foreach (var produto in produtosParaRemover)
                {
                    produtosRestantes.Remove(produto);
                }
            }
        }

        return caixas;
    }
}
