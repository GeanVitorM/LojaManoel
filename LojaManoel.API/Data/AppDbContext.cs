using LojaManoel.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaManoel.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Produtos)
            .WithOne(pr => pr.Pedido)
            .HasForeignKey(pr => pr.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pedido>()
            .HasIndex(p => p.PedidoId)
            .IsUnique();
    }
}
