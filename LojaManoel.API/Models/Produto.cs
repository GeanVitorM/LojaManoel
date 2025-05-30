using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual Pedido? Pedido { get; set; }

    public long Volume => (long)Altura * Largura * Comprimento;
}
