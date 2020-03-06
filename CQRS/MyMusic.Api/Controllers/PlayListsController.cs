using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Queries;
using MyMusic.Application.Read.Model;
using MyMusic.QueryCreators;
using MyMusic.Requests;
using MyMusic.Responses;
using MyMusic.ServiceCreators;

namespace MyMusic.Controllers {

    [Route("playlists")]
    public class PlaylistsController: Controller {
        private readonly PlayListServiceCreator playListServiceCreator;
        private readonly PlayListQueryCreator playListQueryCreator;

        public PlaylistsController(PlayListServiceCreator playListServiceCreator, PlayListQueryCreator playListQueryCreator) {
            this.playListServiceCreator = playListServiceCreator;
            this.playListQueryCreator = playListQueryCreator;
        }

        [HttpGet]
        public ActionResult GetAllPlaylist() {
            var query = playListQueryCreator.CreateGetAllPlayListQuery();
            var result = query.Execute();
            return this.BuildResponseOfType<ListOfPlayListsResponse, ListOfPlayLists>(result);
        }
        
        [HttpGet("{playlistId}")]
        public ActionResult GetPlaylist(string playlistId) {
            var query = playListQueryCreator.CreateGetPlayListQuery();
            var result = query.Get(playlistId);
            return this.BuildResponseOfType<PlayListResponse, PlayList>(result);
        }

        [HttpPost]
        public ActionResult CreatePlayList([FromBody]CreatePlayListRequest createPlayListRequest) {
            var service = playListServiceCreator.CreateCreatePlayListService();
            var result = service.Execute(createPlayListRequest.PlayListName);
            return this.BuildResponseFrom(result);
        }
                
        [HttpPut("{playlistId}/name")]
        public ActionResult RenamePlaylist(string playlistId, [FromBody] RenamePlayListNameRequest renamePlayListNameRequest) {
            var service = playListServiceCreator.CreateRenamePlayListService();
            var result = service.Execute(playlistId, renamePlayListNameRequest.NewPlayListName);
            return this.BuildResponseFrom(result);
        }
        
        [HttpPut("{playlistId}/imageUrl")]
        public ActionResult RenamePlaylist(string playlistId, [FromBody] AddImageUrlToPlayListRequest renamePlayListNameRequest) {
            var service = playListServiceCreator.CreateAddImageUrlPlayListService();
            var result = service.Execute(playlistId, renamePlayListNameRequest.NewImageUrl);
            return this.BuildResponseFrom(result);
        }
        
        [HttpDelete("{playlistId}")]
        public ActionResult Delete(string playlistId) {
            var service = playListServiceCreator.CreateArchivePlayListService();
            var result = service.Execute(playlistId); 
            return this.BuildResponseFrom(result);
        }

    }
}