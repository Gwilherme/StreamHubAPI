using API.Connection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private static readonly List<Item> itens = new List<Item>
        {
            new Item {Id = 1, Name = "Lapis"},
            new Item {Id = 2, Name = "Caneta"}
        };

        [HttpGet("teste")]
        public void Teste()
        {
            var controler = new ConnectionController();
            controler.addConteudo();
        }
        
        [HttpGet]
        public ActionResult<List<Item>> GetAll() => itens;

        [HttpGet("{id}")]
        public ActionResult<Item> GetById(int id) {
            var item = itens.Find(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("index/{id}")]
        public ActionResult<Item> GetIndex(int id) {
            Item item = itens[id];
            if (item != null)
            {
                return item;
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("{id}")]
        public ActionResult<string> Post(int id)
        {
            if(itens.Find(itens => itens.Id == id) == null)
            {
                itens.Add(new Item { Id = id, Name = "Novo Item adicionado" });
                return itens.ToString();
            }
            else
            {
                return "Não foi possivel gravar esse id";
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Item> Put(int id)
        {
            Item item = itens.Find(i => i.Id == id);
            if (item != null)
            {
                item.Name = "Item Atualizado";
                return item;
            }

            return NotFound();
        }
    }
}
