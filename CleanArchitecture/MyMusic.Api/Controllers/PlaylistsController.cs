using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Services;
using MyMusic.Infrastructure.Persistence;
using MyMusic.Model;
using MyMusic.Requests;

namespace MyMusic.Controllers {

    [Route("[controller]")]
    public class PlaylistsController: Controller {

        [HttpGet("{playlistId}")]
        public PlayList Get(string playlistId) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter);
            return playListService.Get(playlistId);
        }
        
        [HttpPost]
        public void Create([FromBody]CreatePlayListRequest createPlayListRequest) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter);
            playListService.Create(createPlayListRequest.PlayListName);
        }
        
        [HttpPut("{playlistId}")]
        public void Update(string playlistId,[FromBody]ChangePlayListNameRequest changePlayListNameRequest ) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter);
            playListService.ChangeName(playlistId, changePlayListNameRequest.NewPlayListName);
        }
        
        [HttpDelete("{playlistId}")]
        public void Delete(string playlistId) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter);
            playListService.Delete(playlistId);                     
        }
    }
}