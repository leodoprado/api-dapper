using api_dapper.Dto;
using api_dapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface) 
        { 
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();

            if (usuarios.Status == false)
            {
                return NotFound(usuarios);  
            }

            return Ok(usuarios);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int usuarioId)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(usuarioId);
            
            if (usuario.Status == false)
            {
                return NotFound(usuario);
            }
            
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            var usuario = await _usuarioInterface.CriarUsuario(usuarioCriarDto);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            var usuario = await _usuarioInterface.EditarUsuario(usuarioEditarDto);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            var usuario = await _usuarioInterface.RemoverUsuario(usuarioId);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }
    }
}
