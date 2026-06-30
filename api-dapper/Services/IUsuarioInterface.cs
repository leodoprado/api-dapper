using api_dapper.Dto;
using api_dapper.Models;

namespace api_dapper.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios();
    }
}
