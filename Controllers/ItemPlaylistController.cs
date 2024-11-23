using API.Connection;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPlaylistController : ControllerBase
    {

        [HttpGet()]
        public List<ItemPlaylist> GetAllItemPlaylists()
        {
            ItemPlaylistConnection ItemPlaylistConnection = new ItemPlaylistConnection();
            return ItemPlaylistConnection.GetAllItemPlaylists();
        }

        [HttpPost("{nome}/{email}")]
        public void AddItemPlaylist(string nome, string email)
        {
            ItemPlaylistConnection ItemPlaylistConnection = new ItemPlaylistConnection();
            ItemPlaylistConnection.addItemPlaylist(nome, email);
        }

        [HttpGet("{id}")]
        public ItemPlaylist GetItemPlaylistByID(int id)
        {
            ItemPlaylistConnection ItemPlaylistConnection = new ItemPlaylistConnection();
            return ItemPlaylistConnection.GetItemPlaylistByID(id);
        }

        [HttpPut("{id}")]
        public void UpdateItemPlaylist(int id, string nome, string email)
        {
            ItemPlaylistConnection ItemPlaylistConnection = new ItemPlaylistConnection();
            ItemPlaylistConnection.UpdateItemPlaylist(id, nome, email);
        }

        [HttpDelete("{id}")]
        public void DeleteItemPlaylist(int id)
        {
            ItemPlaylistConnection ItemPlaylistConnection = new ItemPlaylistConnection();
            ItemPlaylistConnection.DeleteItemPlaylist(id);
        }


    }
}
