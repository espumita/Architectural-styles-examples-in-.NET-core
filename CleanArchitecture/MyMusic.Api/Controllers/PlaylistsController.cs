using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Services;
using MyMusic.Infrastructure.Persistence;
using MyMusic.Requests;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    [Route("[controller]")]
    public class PlaylistsController: Controller {

        [HttpGet("{playlistId}")]
        public PlayListResponse Get(string playlistId) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter);
            var playList = playListService.Get(playlistId);
            return PlayListResponse.From(playList);
        }
        
        [HttpPost]
        public void Create([FromBody]CreatePlayListRequest createPlayListRequest) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter);
            playListService.Create(createPlayListRequest.PlayListName);
        }
        
        [HttpPut("{playlistId}")]
        public void Update(string playlistId,[FromBody]ChangePlayListNameRequest changePlayListNameRequest ) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter);
            playListService.ChangeName(playlistId, changePlayListNameRequest.NewPlayListName);
        }
        
        [HttpDelete("{playlistId}")]
        public void Delete(string playlistId) {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            var playListService = new PlayListService(playListDatabaseAdapter);
            playListService.Delete(playlistId);                     
        }
        
        [HttpPost("{playlistId}/Tracks/{trackId}")]
        public void AddTrack(string playlistId, string trackId) {
            var tracksDatabaseAdapter = new TracksDatabaseAdapter();
            var tracksService = new TracksService(tracksDatabaseAdapter);
            tracksService.AddToPlayList(trackId, playlistId);
        }

        [HttpDelete("{playlistId}/Tracks/{trackId}")]
        public void DeleteTrack(string playlistId, string trackId) {
            var tracksDatabaseAdapter = new TracksDatabaseAdapter();
            var tracksService = new TracksService(tracksDatabaseAdapter);
            tracksService.DeleteFromPlayList(trackId, playlistId);        
        }
    }
}