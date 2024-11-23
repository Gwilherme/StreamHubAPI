using API.Connection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteudoController : ControllerBase
    {

        [HttpGet()]
        public List<Conteudo> GetAllConteudos()
        {
            ConteudoConnection ConteudoConnection = new ConteudoConnection();
            return ConteudoConnection.GetAllConteudos();
        }

        [HttpPost("{nome}/{titulo}/{criador}")]
        public void AddConteudo(string tipo, string titulo, int criador)
        {
            ConteudoConnection ConteudoConnection = new ConteudoConnection();
            ConteudoConnection.addConteudo(tipo, titulo, criador);
        }

        [HttpGet("{id}")]
        public Conteudo GetConteudoByID(int id)
        {
            ConteudoConnection ConteudoConnection = new ConteudoConnection();
            return ConteudoConnection.GetConteudoByID(id);
        }

        [HttpPut("{id}")]
        public void UpdateConteudo(int id, string tipo, string titulo)
        {
            ConteudoConnection ConteudoConnection = new ConteudoConnection();
            ConteudoConnection.UpdateConteudo(id, tipo, titulo);
        }

        [HttpDelete("{id}")]
        public void DeleteConteudo(int id)
        {
            ConteudoConnection ConteudoConnection = new ConteudoConnection();
            ConteudoConnection.DeleteConteudo(id);
        }


    }
}
