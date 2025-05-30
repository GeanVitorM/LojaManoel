using System.ComponentModel.DataAnnotations;

namespace LojaManoel.API.Models;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PedidoId { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
