using System.ComponentModel.DataAnnotations;

namespace LojaManoel.API.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ProdutoId { get; set; } = string.Empty;

    [Required]
    public int Altura { get; set; }

    [Required]
    public int Largura { get; set; }

    [Required]
    public int Comprimento { get; set; }

    public int PedidoId { get; set; }
    public virtual Pedido Pedido { get; set; } = null!;

    public long Volume => (long)Altura * Largura * Comprimento;
}
