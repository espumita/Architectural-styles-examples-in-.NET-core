using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Services;
using MyMusic.Domain;
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

        [HttpGet]
        public ActionResult GetAllPlaylist() {
            var service = playListServiceCreator.CreateGetAllPlayListService();
            var result = service.Execute();
            return this.BuildResponseOfType<ListOfPlayListsResponse, ListOfPlayLists>(result);
        }
        
        [HttpGet("{playlistId}")]
        public ActionResult GetPlaylist(string playlistId) {
            var service = playListServiceCreator.CreateGetPlayListService();
            var result = service.Get(playlistId);
            return this.BuildResponseOfType<PlayListResponse, PlayList>(result);
        }

        [HttpPost]
        public ActionResult CreatePlayList([FromBody]CreatePlayListRequest request) {
            var service = playListServiceCreator.CreateCreatePlayListService();
            var result = service.Execute(request.PlayListName);
            return this.BuildResponseFrom(result);
        }
                
        [HttpPut("{playlistId}/name")]
        public ActionResult RenamePlaylist(string playlistId, [FromBody] RenamePlayListNameRequest request) {
            var service = playListServiceCreator.CreateRenamePlayListService();
            var result = service.Execute(playlistId, request.NewPlayListName);
            return this.BuildResponseFrom(result);
        }
        
        [HttpPut("{playlistId}/imageUrl")]
        public ActionResult ChangePlayListImageUrl(string playlistId, [FromBody] AddImageUrlToPlayListRequest request) {
            var service = playListServiceCreator.CreateAddImageUrlPlayListService();
            var result = service.Execute(playlistId, request.NewImageUrl);
            return this.BuildResponseFrom(result);
        }
        
        [HttpDelete("{playlistId}")]
        public ActionResult ArchivePlayList(string playlistId) {
            var service = playListServiceCreator.CreateArchivePlayListService();
            var result = service.Execute(playlistId); 
            return this.BuildResponseFrom(result);
        }

    }
}