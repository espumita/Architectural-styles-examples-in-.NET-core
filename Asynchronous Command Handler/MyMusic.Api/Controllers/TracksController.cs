using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Read.Model;
using MyMusic.CommandHandlerCreators;
using MyMusic.QueryCreators;
using MyMusic.Responses;

namespace MyMusic.Controllers {
    
    public class TracksController : Controller {
        
        private readonly TracksCommandHandlerCreator tracksCommandHandlerCreator;
        private readonly TracksQueryCreator tracksQueryCreator;

        public TracksController(TracksCommandHandlerCreator tracksCommandHandlerCreator, TracksQueryCreator tracksQueryCreator) {
            this.tracksCommandHandlerCreator = tracksCommandHandlerCreator;
            this.tracksQueryCreator = tracksQueryCreator;
        }

        [HttpGet("tracks/{trackId}")]
        public ActionResult GetTrack(string trackId) {
            var query = tracksQueryCreator.CreateGetTrackQuery();
            var result = query.Get(trackId);
            return this.BuildResponseOfType<TrackResponse, Track>(result);
        }
        
        [HttpPost("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult AddTrack(string playlistId, string trackId) {
            var service = tracksCommandHandlerCreator.CreateAddTrackToPlayListCommandHandler();
            var result = service.Execute(trackId, playlistId);
            return this.BuildResponseFrom(result);
        }

        [HttpDelete("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult RemoveTrack(string playlistId, string trackId) {
            var service = tracksCommandHandlerCreator.CreateRemoveTrackFromPLayListCommandHandler();
            var result = service.Execute(trackId, playlistId);
            return this.BuildResponseFrom(result);
        }
        
    }
}