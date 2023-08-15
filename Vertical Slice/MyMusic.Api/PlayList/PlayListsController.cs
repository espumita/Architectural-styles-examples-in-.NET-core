using Microsoft.AspNetCore.Mvc;
using MyMusic.PlayList.Features.ArchivePlayList;
using MyMusic.PlayList.Features.ChangePlayListImageUrl;
using MyMusic.PlayList.Features.CreatePLayList;
using MyMusic.PlayList.Features.GetAllPlayListQuery;
using MyMusic.PlayList.Features.GetPlayListQuery;
using MyMusic.PlayList.Features.RenamePlaylist;
using MyMusic.Shared;
using MyMusic.Shared.Ports;

namespace MyMusic.PlayList {

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
            return this.BuildResponseOfType<PlayListResponse, Features.GetPlayListQuery.PlayList>(result);
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