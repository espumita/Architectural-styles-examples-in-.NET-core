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
                
        [HttpPut("{playlistId}/name")]
        public void RenamePlaylist(string playlistId, [FromBody] RenamePlayListNameRequest RenamePlayListNameRequest) {
            var playListService = playListServiceCreator.CreateRenamePlayListService();
            playListService.Execute(playlistId, RenamePlayListNameRequest.NewPlayListName);
        }
        
        [HttpPut("{playlistId}/imageUrl")]
        public void RenamePlaylist(string playlistId, [FromBody] AddImageUrlToPlayListRequest RenamePlayListNameRequest) {
            var playListService = playListServiceCreator.CreateAddImageUrlPlayListService();
            playListService.Execute(playlistId, RenamePlayListNameRequest.NewImageUrl);
        }
        
        [HttpDelete("{playlistId}")]
        public void Delete(string playlistId) {
            var playListService = playListServiceCreator.CreateArchivePlayListService();
            playListService.Execute(playlistId);                     
        }

    }
}