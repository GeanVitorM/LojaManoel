namespace LojaManoel.API.DTOs;

public class EmbalagemOutputDto
{
    public List<PedidoEmbalagemDto> Pedidos { get; set; } = new();
}

public class PedidoEmbalagemDto
{
    public int PedidoId { get; set; }
    public List<CaixaEmbalagemDto> Caixas { get; set; } = new();
}

public class CaixaEmbalagemDto
{
    public string? CaixaId { get; set; }
    public List<string> Produtos { get; set; } = new();
    public string? Observacao { get; set; }
}
