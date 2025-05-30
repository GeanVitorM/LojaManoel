using System.Text.Json.Serialization;

namespace LojaManoel.API.DTOs;

public class EmbalagemOutputDto
{
    [JsonPropertyName("pedidos")]
    public List<PedidoEmbalagemDto> Pedidos { get; set; } = new();
}

public class PedidoEmbalagemDto
{
    [JsonPropertyName("pedido_id")]
    public int PedidoId { get; set; }

    [JsonPropertyName("caixas")]
    public List<CaixaEmbalagemDto> Caixas { get; set; } = new();
}

public class CaixaEmbalagemDto
{
    [JsonPropertyName("caixa_id")]
    public string? CaixaId { get; set; }

    [JsonPropertyName("produtos")]
    public List<string> Produtos { get; set; } = new();

    [JsonPropertyName("observacao")]
    public string? Observacao { get; set; }
}
