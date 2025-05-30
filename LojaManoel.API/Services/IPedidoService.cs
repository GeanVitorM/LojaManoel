using LojaManoel.API.Models;

namespace LojaManoel.API.Services;

public interface IPedidoService
{
    Task<IEnumerable<Pedido>> ObterTodos();
    Task<Pedido?> ObterPorId(int id);
    Task<Pedido> Criar(Pedido pedido);
    Task<Pedido?> Atualizar(int id, Pedido pedido);
    Task<bool> Deletar(int id);
}
