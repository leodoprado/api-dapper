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

        public async Task<ResponseModel<UsuarioListarDto>> BuscarUsuarioPorId(int usuarioId)
        {
            ResponseModel<UsuarioListarDto> response = new ResponseModel<UsuarioListarDto>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = usuarioId });

                if (usuarioBanco == null)
                {
                    response.Mensagem = "Usuário não encontrado.";
                    response.Status = false;
                    return response;
                }
                var usuarioMapeado = _mapper.Map<UsuarioListarDto>(usuarioBanco);
                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuário encontrado com sucesso.";
            }
            return response;
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

        public async Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync("INSERT INTO Usuarios (Nome, Email, Cargo, Salario, CPF, Ativo, Senha)" +
                                                                  "VALUES (@Nome, @Email, @Cargo, @Salario, @CPF, @Ativo, @Senha)", usuarioCriarDto);

                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Erro ao criar usuário.";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuário criado com sucesso.";
         
            }
            return response;
        }

        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            var usuariosBanco = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");
            return usuariosBanco;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync("UPDATE Usuarios SET Nome = @Nome, " +
                                                                  "                    Email = @Email, " +
                                                                  "                    Cargo = @Cargo, " +
                                                                  "                    Salario = @Salario, " +
                                                                  "                    CPF = @CPF, " +
                                                                  "                    Ativo = @Ativo " +
                                                                  "WHERE Id = @Id", usuarioEditarDto);

                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Erro ao editar usuário.";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuário editado com sucesso.";
            }

            return response;
        }
    }
}
