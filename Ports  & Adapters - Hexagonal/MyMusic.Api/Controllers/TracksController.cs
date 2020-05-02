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
            var service = tracksServiceCreator.CreateGetTrackService();
            var result = service.Get(trackId);
            return this.BuildResponseOfType<TrackResponse, Track>(result);
        }
        
        [HttpPost("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult AddTrackToPLayList(string playlistId, string trackId) {
            var service = tracksServiceCreator.CreateAddTrackToPlayListService();
            var result = service.Execute(trackId, playlistId);
            return this.BuildResponseFrom(result);
        }

        [HttpDelete("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult RemoveTrackFromPlayList(string playlistId, string trackId) {
            var service = tracksServiceCreator.CreateRemoveTrackFromPLayListService();
            var result = service.Execute(trackId, playlistId);
            return this.BuildResponseFrom(result);
        }
        
    }
}