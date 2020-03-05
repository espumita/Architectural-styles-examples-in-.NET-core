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
        public ActionResult CreatePlayList([FromBody]CreatePlayListRequest createPlayListRequest) {
            var playListService = playListServiceCreator.CreateCreatePlayListService();
            var result = playListService.Execute(createPlayListRequest.PlayListName);
            return this.BuildResponseFrom(result);
        }
                
        [HttpPut("{playlistId}/name")]
        public ActionResult RenamePlaylist(string playlistId, [FromBody] RenamePlayListNameRequest renamePlayListNameRequest) {
            var playListService = playListServiceCreator.CreateRenamePlayListService();
            var result = playListService.Execute(playlistId, renamePlayListNameRequest.NewPlayListName);
            return this.BuildResponseFrom(result);
        }
        
        [HttpPut("{playlistId}/imageUrl")]
        public ActionResult RenamePlaylist(string playlistId, [FromBody] AddImageUrlToPlayListRequest renamePlayListNameRequest) {
            var playListService = playListServiceCreator.CreateAddImageUrlPlayListService();
            var result = playListService.Execute(playlistId, renamePlayListNameRequest.NewImageUrl);
            return this.BuildResponseFrom(result);
        }
        
        [HttpDelete("{playlistId}")]
        public ActionResult Delete(string playlistId) {
            var playListService = playListServiceCreator.CreateArchivePlayListService();
            var result = playListService.Execute(playlistId); 
            return this.BuildResponseFrom(result);
        }

    }
}