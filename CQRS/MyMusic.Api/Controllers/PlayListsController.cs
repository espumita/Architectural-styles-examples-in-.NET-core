﻿using Microsoft.AspNetCore.Mvc;
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
            var playListService = playListQueryCreator.CreateGetAllPlayListService();
            var result = playListService.Execute();
            return this.BuildResponseOfType<ListOfPlayListsResponse, ListOfPlayLists>(result);
        }
        
        [HttpGet("{playlistId}")]
        public ActionResult GetPlaylist(string playlistId) {
            var playListService = playListQueryCreator.CreateGetPlayListService();
            var result = playListService.Get(playlistId);
            return this.BuildResponseOfType<PlayListResponse, PlayList>(result);
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