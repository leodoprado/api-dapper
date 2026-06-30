using api_dapper.Dto;
using api_dapper.Models;
using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;

namespace api_dapper.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");
            
                if (usuariosBanco.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuário encontrado.";
                    response.Status = false;
                    return response;
                }

                var usuarioMapeado = _mapper.Map<List<UsuarioListarDto>>(usuariosBanco);
                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuários encontrados com sucesso.";
            }
            return response;
        }
    }
}
