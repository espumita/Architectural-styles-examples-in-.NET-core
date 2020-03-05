using Microsoft.AspNetCore.Mvc;
using MyMusic.Requests;
using MyMusic.Responses;
using MyMusic.ServiceCreators;

namespace MyMusic.Controllers {

    [Route("playlists")]
    public class PlaylistsController: Controller {
        private readonly PlayListServiceCreator playListServiceCreator;

        public PlaylistsController(PlayListServiceCreator playListServiceCreator) {
            this.playListServiceCreator = playListServiceCreator;
        }

        [HttpGet("{playlistId}")]
        public PlayListResponse GetPlaylist(string playlistId) {
            var playListService = playListServiceCreator.CreateGetPlayListService();
            var playList = playListService.Get(playlistId);
            return PlayListResponse.From(playList);
        }
        
        [HttpPost]
        public void CreatePlayList([FromBody]CreatePlayListRequest createPlayListRequest) {
            var playListService = playListServiceCreator.CreateCreatePlayListService();
            playListService.Execute(createPlayListRequest.PlayListName);
        }
                
        [HttpPut("{playlistId}")]
        public void ChangePlaylistName(string playlistId, [FromBody] ChangePlayListNameRequest changePlayListNameRequest) {
            var playListService = playListServiceCreator.CreateChangePlayListService();
            playListService.Execute(playlistId, changePlayListNameRequest.NewPlayListName);
        }
        
        [HttpDelete("{playlistId}")]
        public void Delete(string playlistId) {
            var playListService = playListServiceCreator.CreateArchivePlayListService();
            playListService.Execute(playlistId);                     
        }

    }
}