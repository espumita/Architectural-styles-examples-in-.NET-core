using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Read.Model;
using MyMusic.QueryCreators;
using MyMusic.Responses;
using MyMusic.ServiceCreators;

namespace MyMusic.Controllers {
    
    public class TracksController : Controller {
        
        private readonly TracksServiceCreator tracksServiceCreator;
        private readonly TracksQueryCreator tracksQueryCreator;

        public TracksController(TracksServiceCreator tracksServiceCreator, TracksQueryCreator tracksQueryCreator) {
            this.tracksServiceCreator = tracksServiceCreator;
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
            var service = tracksServiceCreator.CreateAddTrackToPlayListService();
            var result = service.Execute(trackId, playlistId);
            return this.BuildResponseFrom(result);
        }

        [HttpDelete("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult RemoveTrack(string playlistId, string trackId) {
            var service = tracksServiceCreator.CreateRemoveTrackFromPLayListService();
            var result = service.Execute(trackId, playlistId);
            return this.BuildResponseFrom(result);
        }
        
    }
}