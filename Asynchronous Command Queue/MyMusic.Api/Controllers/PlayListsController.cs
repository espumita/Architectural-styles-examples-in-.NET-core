﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Commands;
using MyMusic.Application.Ports;
using MyMusic.Application.Queries;
using MyMusic.Application.Read.Model;
using MyMusic.Domain.Events;
using MyMusic.QueryCreators;
using MyMusic.Requests;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    [Route("playlists")]
    public class PlaylistsController: Controller {
        private readonly PlayListQueryCreator playListQueryCreator;
        private readonly SignalRWebsocketAdapter websocketAdapter;
        private readonly CommandQueuePort commandQueue;

        public PlaylistsController(CommandQueuePort commandQueue, PlayListQueryCreator playListQueryCreator, SignalRWebsocketAdapter websocketAdapter) {
            this.commandQueue = commandQueue;
            this.playListQueryCreator = playListQueryCreator;
            this.websocketAdapter = websocketAdapter;
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
        public async Task<ActionResult> CreatePlayList([FromBody]CreatePlayListRequest request) {
            //commandQueue.Queue(new CreatePLayList(request.PlayListName));
            await websocketAdapter.PushMessageWithEvent(new PlayListHasBeenCreated("wtf", "isthisshit"));
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