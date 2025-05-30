namespace LojaManoel.API.Models;

public class Caixa
{
    public string Id { get; set; } = string.Empty;
    public int Altura { get; set; }
    public int Largura { get; set; }
    public int Comprimento { get; set; }
    public long Volume => (long)Altura * Largura * Comprimento;

    public bool CabeProduto(Produto produto)
    {
        return produto.Altura <= Altura &&
               produto.Largura <= Largura &&
               produto.Comprimento <= Comprimento;
    }
}
