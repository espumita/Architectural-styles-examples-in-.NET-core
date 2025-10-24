using Microsoft.AspNetCore.Mvc;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePLayList;
using MyMusic.PlayLists.Features.GetAllPlayListQuery;
using MyMusic.PlayLists.Features.GetPlayListQuery;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.PlayLists {

    [Route("playlists")]
    public class PlaylistsController: Controller {
        private readonly PlayListQueryCreator playListQueryCreator;
        private readonly CommandQueue commandQueue;

        public PlaylistsController(CommandQueue commandQueue, PlayListQueryCreator playListQueryCreator) {
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