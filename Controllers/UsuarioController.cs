using API.Connection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpGet()]
        public List<Usuario> GetAllUsuarios()
        {
            UsuarioConnection UsuarioConnection = new UsuarioConnection();
            return UsuarioConnection.GetAllUsuarios();
        }

        [HttpPost("{nome}/{email}")]
        public void AddUsuario(string nome, string email)
        {
            UsuarioConnection UsuarioConnection = new UsuarioConnection();
            UsuarioConnection.addUsuario(nome, email);
        }

        [HttpGet("{id}")]
        public Usuario GetUsuarioByID(int id)
        {
            UsuarioConnection UsuarioConnection = new UsuarioConnection();
            return UsuarioConnection.GetUsuarioByID(id);
        }

        [HttpPut("{id}")]
        public void UpdateUsuario(int id, string nome, string email)
        {
            UsuarioConnection UsuarioConnection = new UsuarioConnection();
            UsuarioConnection.UpdateUsuario(id, nome, email);
        }

        [HttpDelete("{id}")]
        public void DeleteUsuario(int id)
        {
            UsuarioConnection UsuarioConnection = new UsuarioConnection();
            UsuarioConnection.DeleteUsuario(id);
        }


    }
}
