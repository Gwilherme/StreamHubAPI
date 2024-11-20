using API.Connection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        // teste git brisa

        [HttpGet("adicionar/{nome}/{email}")]
        public void Adicionar(string nome, string email)
        {
            var controler = new UsuarioConnection();
            controler.addUsuario(nome, email);
        }
        
    }
}
