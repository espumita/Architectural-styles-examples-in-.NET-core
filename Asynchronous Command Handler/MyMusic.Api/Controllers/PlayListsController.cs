using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Commands;
using MyMusic.Application.Ports;
using MyMusic.Application.Queries;
using MyMusic.Application.Read.Model;
using MyMusic.QueryCreators;
using MyMusic.Requests;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    [Route("playlists")]
    public class PlaylistsController: Controller {
        private readonly PlayListQueryCreator playListQueryCreator;
        private readonly CommandQueuePort commandQueuePort;

        public PlaylistsController(CommandQueuePort commandQueuePort, PlayListQueryCreator playListQueryCreator) {
            this.commandQueuePort = commandQueuePort;
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
        public ActionResult ChangePlayListImageUrl(string playlistId, [FromBody] AddImageUrlToPlayListRequest request) {
            commandQueuePort.Queue(new ChangePlayListImageUrl(playlistId, request.NewImageUrl));
            return Ok();
        }
        
        [HttpDelete("{playlistId}")]
        public ActionResult ArchivePlayList(string playlistId) {
            commandQueuePort.Queue(new ArchivePlayList(playlistId));
            return Ok();
        }

    }
}