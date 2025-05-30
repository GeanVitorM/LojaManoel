namespace LojaManoel.API.DTOs;

public class PedidoInputDto
{
    public List<PedidoDto> Pedidos { get; set; } = new();
}

public class PedidoDto
{
    public int PedidoId { get; set; }
    public List<ProdutoDto> Produtos { get; set; } = new();
}

public class ProdutoDto
{
    public string ProdutoId { get; set; } = string.Empty;
    public DimensoesDto Dimensoes { get; set; } = new();
}

public class DimensoesDto
{
    public int Altura { get; set; }
    public int Largura { get; set; }
    public int Comprimento { get; set; }
}
