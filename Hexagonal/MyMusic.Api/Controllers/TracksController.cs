using Microsoft.AspNetCore.Mvc;
using MyMusic.Responses;
using MyMusic.ServiceCreators;

namespace MyMusic.Controllers {
    
    public class TracksController : Controller {
        
        private readonly TracksServiceCreator tracksServiceCreator;

        public TracksController(TracksServiceCreator tracksServiceCreator) {
            this.tracksServiceCreator = tracksServiceCreator;
        }

        [HttpGet("tracks/{trackId}")]
        public TrackResponse GetTrack(string trackId) {
            var tracksService = tracksServiceCreator.CreateGetTrackService();
            var track = tracksService.Get(trackId);
            return TrackResponse.From(track);
        }
        
        [HttpPost("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult AddTrack(string playlistId, string trackId) {
            var tracksService = tracksServiceCreator.CreateAddTrackToPlayListService();
            var result = tracksService.Execute(trackId, playlistId);
            return this.BuildResponseFrom(result);
        }

        [HttpDelete("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult DeleteTrack(string playlistId, string trackId) {
            var tracksService = tracksServiceCreator.CreateDeleteTrackFromPLayListService();
            var result = tracksService.Execute(trackId, playlistId);
            return this.BuildResponseFrom(result);
        }
        
    }
}