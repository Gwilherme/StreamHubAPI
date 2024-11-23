using API.Connection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistRepository : ControllerBase
    {
        [HttpGet()]
        public List<Playlist> GetAllPlaylists()
        {
            PlaylistConnection playlistConnection = new PlaylistConnection();
            return playlistConnection.GetAllPlaylists();
        }

        [HttpPost("{nome}/{usuario}")]
        public void AddPlaylist(string nome, int usuario)
        {
            PlaylistConnection playlistConnection = new PlaylistConnection();
            playlistConnection.addPlaylist(nome, usuario);
        }

        [HttpGet("{id}")]
        public Playlist GetPlaylistByID(int id)
        {
            PlaylistConnection playlistConnection = new PlaylistConnection();
            return playlistConnection.GetPlaylistByID(id);
        }

        [HttpPut("{id}")]
        public void UpdatePlaylist(int id, string nome)
        {
            PlaylistConnection playlistConnection = new PlaylistConnection();
            playlistConnection.UpdatePlaylist(id, nome);
        }

        [HttpDelete("{id}")]
        public void DeletePlaylist(int id)
        {
            PlaylistConnection playlistConnection = new PlaylistConnection();
            playlistConnection.DeletePlaylist(id);
        }

    }
}
