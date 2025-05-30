using LojaManoel.API.Data;
using LojaManoel.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaManoel.API.Services;

public class PedidoService : IPedidoService
{
    private readonly AppDbContext _context;

    public PedidoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pedido>> ObterTodos()
    {
        return await _context.Pedidos.Include(p => p.Produtos).ToListAsync();
    }

    public async Task<Pedido?> ObterPorId(int id)
    {
        return await _context.Pedidos.Include(p => p.Produtos)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pedido> Criar(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }

    public async Task<Pedido?> Atualizar(int id, Pedido pedido)
    {
        var pedidoExistente = await _context.Pedidos.Include(p => p.Produtos)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedidoExistente == null) return null;

        pedidoExistente.PedidoId = pedido.PedidoId;

        _context.Produtos.RemoveRange(pedidoExistente.Produtos);

        pedidoExistente.Produtos = pedido.Produtos;

        await _context.SaveChangesAsync();
        return pedidoExistente;
    }

    public async Task<bool> Deletar(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido == null) return false;

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();
        return true;
    }
}
