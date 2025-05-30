using LojaManoel.API.DTOs;

namespace LojaManoel.API.Services;

public interface IEmbalagemService
{
    Task<EmbalagemOutputDto> ProcessarEmbalagem(PedidoInputDto input);
}
