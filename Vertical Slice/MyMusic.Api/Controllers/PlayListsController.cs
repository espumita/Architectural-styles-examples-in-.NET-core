using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Queries;
using MyMusic.Application.Write.Commands;
using MyMusic.Application.Write.Ports;
using MyMusic.QueryCreators;
using MyMusic.Requests;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    [Route("playlists")]
    public class PlaylistsController: Controller {
        private readonly PlayListQueryCreator playListQueryCreator;
        private readonly CommandQueuePort commandQueue;

        public PlaylistsController(CommandQueuePort commandQueue, PlayListQueryCreator playListQueryCreator) {
            this.commandQueue = commandQueue;
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
            commandQueue.Queue(new CreatePLayList(request.PlayListName));
            return Ok();
        }
                
        [HttpPut("{playlistId}/name")]
        public ActionResult RenamePlaylist(string playlistId, [FromBody] RenamePlayListNameRequest request) {
            commandQueue.Queue(new RenamePlaylist(playlistId, request.NewPlayListName));
            return Ok();
        }
        
        [HttpPut("{playlistId}/imageUrl")]
        public ActionResult ChangePlayListImageUrl(string playlistId, [FromBody] AddImageUrlToPlayListRequest request) {
            commandQueue.Queue(new ChangePlayListImageUrl(playlistId, request.NewImageUrl));
            return Ok();
        }
        
        [HttpDelete("{playlistId}")]
        public ActionResult ArchivePlayList(string playlistId) {
            commandQueue.Queue(new ArchivePlayList(playlistId));
            return Ok();
        }

    }
}