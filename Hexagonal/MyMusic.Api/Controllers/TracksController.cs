using Microsoft.AspNetCore.Mvc;
using MyMusic.Domain;
using MyMusic.Responses;
using MyMusic.ServiceCreators;

namespace MyMusic.Controllers {
    
    public class TracksController : Controller {
        
        private readonly TracksServiceCreator tracksServiceCreator;

        public TracksController(TracksServiceCreator tracksServiceCreator) {
            this.tracksServiceCreator = tracksServiceCreator;
        }

        [HttpGet("tracks/{trackId}")]
        public ActionResult GetTrack(string trackId) {
            var tracksService = tracksServiceCreator.CreateGetTrackService();
            var result = tracksService.Get(trackId);
            return this.BuildResponseOfType<TrackResponse, Track>(result);
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