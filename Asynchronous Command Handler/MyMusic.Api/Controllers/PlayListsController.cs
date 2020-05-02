using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Commands;
using MyMusic.Application.Ports;
using MyMusic.Application.Queries;
using MyMusic.Application.Read.Model;
using MyMusic.CommandHandlerCreators;
using MyMusic.QueryCreators;
using MyMusic.Requests;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    [Route("playlists")]
    public class PlaylistsController: Controller {
        private readonly PlayListCommandHandlerCreator playListCommandHandlerCreator;
        private readonly PlayListQueryCreator playListQueryCreator;
        private readonly CommandQueuePort commandQueuePort;

        public PlaylistsController(PlayListCommandHandlerCreator playListCommandHandlerCreator, PlayListQueryCreator playListQueryCreator, CommandQueuePort commandQueuePort) {
            this.playListCommandHandlerCreator = playListCommandHandlerCreator;
            this.playListQueryCreator = playListQueryCreator;
            this.commandQueuePort = commandQueuePort;
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
        public ActionResult CreatePlayList([FromBody]CreatePlayListRequest request) {
            commandQueuePort.Queue(new CreatePLayList(request.PlayListName));
            return Ok();
        }
                
        [HttpPut("{playlistId}/name")]
        public ActionResult RenamePlaylist(string playlistId, [FromBody] RenamePlayListNameRequest request) {
            commandQueuePort.Queue(new RenamePlaylist(playlistId, request.NewPlayListName));
            return Ok();

        }
        
        [HttpPut("{playlistId}/imageUrl")]
        public ActionResult RenamePlaylist(string playlistId, [FromBody] AddImageUrlToPlayListRequest renamePlayListNameRequest) {
            var service = playListCommandHandlerCreator.CreateAddImageUrlPlayListCommandHandler();
            var result = service.Execute(playlistId, renamePlayListNameRequest.NewImageUrl);
            return this.BuildResponseFrom(result);
        }
        
        [HttpDelete("{playlistId}")]
        public ActionResult Archive(string playlistId) {
            var service = playListCommandHandlerCreator.CreateArchivePlayListCommandHandler();
            var result = service.Execute(playlistId); 
            return this.BuildResponseFrom(result);
        }

    }
}