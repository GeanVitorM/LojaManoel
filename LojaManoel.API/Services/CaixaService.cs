using LojaManoel.API.Models;

namespace LojaManoel.API.Services;

public class CaixaService : ICaixaService
{
    private readonly List<Caixa> _caixasDisponiveis = new()
    {
        new Caixa { Id = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
        new Caixa { Id = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
        new Caixa { Id = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
    };

    public List<Caixa> ObterCaixasDisponiveis() => _caixasDisponiveis;

    public Caixa? EncontrarCaixaCompativel(Produto produto, List<Produto> produtosJaAlocados)
    {
        var volumeNecessario = produtosJaAlocados.Sum(p => p.Volume) + produto.Volume;

        return _caixasDisponiveis
            .Where(c => c.CabeProduto(produto) &&
                       produtosJaAlocados.All(p => c.CabeProduto(p)) &&
                       c.Volume >= volumeNecessario)
            .OrderBy(c => c.Volume)
            .FirstOrDefault();
    }
}
