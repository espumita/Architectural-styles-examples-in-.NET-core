using Microsoft.AspNetCore.Mvc;

namespace MyMusic.Controllers {

    [Route("[controller]")]
    public class PlaylistsController: Controller {

        [HttpGet("{playlistId}")]
        public void Get(string playlistId) {
            //Get the playlist
        }
        
        [HttpPost]
        public void Create() {
            //Create the playlist
        }
        
        [HttpPut("{playlistId}")]
        public void Update(string playlistId) {
            //ChangeName
        }
        
        [HttpDelete("{playlistId}")]
        public void Delete(string playlistId) {
            //Delete the playlist                        
        }
    }
}