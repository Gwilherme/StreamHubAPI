using API.Connection;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriadorController : ControllerBase
    {

        [HttpGet()]
        public List<Criador> GetAllCriadors()
        {
            CriadorConnection CriadorConnection = new CriadorConnection();
            return CriadorConnection.GetAllCriadors();
        }

        [HttpPost("{nome}")]
        public void AddCriador(string nome)
        {
            CriadorConnection CriadorConnection = new CriadorConnection();
            CriadorConnection.addCriador(nome);
        }

        [HttpGet("{id}")]
        public Criador GetCriadorByID(int id)
        {
            CriadorConnection CriadorConnection = new CriadorConnection();
            return CriadorConnection.GetCriadorByID(id);
        }

        [HttpPut("{id}")]
        public void UpdateCriador(int id, string nome)
        {
            CriadorConnection CriadorConnection = new CriadorConnection();
            CriadorConnection.UpdateCriador(id, nome);
        }

        [HttpDelete("{id}")]
        public void DeleteCriador(int id)
        {
            CriadorConnection CriadorConnection = new CriadorConnection();
            CriadorConnection.DeleteCriador(id);
        }


    }
}
