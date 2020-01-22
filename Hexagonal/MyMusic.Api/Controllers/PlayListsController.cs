using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Services;
using MyMusic.Infrastructure.Http;
using MyMusic.Infrastructure.Persistence;
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
            var playListService = playListServiceCreator.CreatePlayListService();
            var playList = playListService.Get(playlistId);
            return PlayListResponse.From(playList);
        }
        
        [HttpPost]
        public void CreatePlayList([FromBody]CreatePlayListRequest createPlayListRequest) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListCloudHttpAdapter = new MusicCloudApiHttpAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter, playListCloudHttpAdapter);
            playListService.Create(createPlayListRequest.PlayListName);
        }
                
        [HttpPut("{playlistId}")]
        public void ChangePlaylistName(string playlistId, [FromBody] ChangePlayListNameRequest changePlayListNameRequest) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var musicCloudApiHttpAdapter = new MusicCloudApiHttpAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter, musicCloudApiHttpAdapter);
            playListService.ChangeName(playlistId, changePlayListNameRequest.NewPlayListName);
        }
        
        [HttpDelete("{playlistId}")]
        public void Delete(string playlistId) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var musicCloudApiHttpAdapter = new MusicCloudApiHttpAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter, musicCloudApiHttpAdapter);
            playListService.Delete(playlistId);                     
        }
        
        [HttpPost("{playlistId}/tracks/{trackId}")]
        public void AddTrack(string playlistId, string trackId) {
            var tracksDatabaseAdapter = new TracksDatabaseAdapter();
            var musicCloudApiHttpAdapter = new MusicCloudApiHttpAdapter();
            var tracksService = new TracksService(tracksDatabaseAdapter, musicCloudApiHttpAdapter);
            tracksService.AddToPlayList(trackId, playlistId);
        }

        [HttpDelete("{playlistId}/tracks/{trackId}")]
        public void DeleteTrack(string playlistId, string trackId) {
            var tracksDatabaseAdapter = new TracksDatabaseAdapter();
            var tracksService = new TracksService(tracksDatabaseAdapter);
            tracksService.DeleteFromPlayList(trackId, playlistId);        
        }
    }
}