using LojaManoel.API.Models;

namespace LojaManoel.API.Services;

public interface ICaixaService
{
    List<Caixa> ObterCaixasDisponiveis();
    Caixa? EncontrarCaixaCompativel(Produto produto, List<Produto> produtosJaAlocados);
}
