using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Write.Commands;
using MyMusic.Application.Write.Ports;
using MyMusic.QueryCreators;
using MyMusic.Responses;

namespace MyMusic.Controllers {
    
    public class TracksController : Controller {
        
        private readonly CommandQueuePort commandQueue;
        private readonly TracksQueryCreator tracksQueryCreator;

        public TracksController(CommandQueuePort commandQueue, TracksQueryCreator tracksQueryCreator) {
            this.commandQueue = commandQueue;
            this.tracksQueryCreator = tracksQueryCreator;
        }

        [HttpGet("tracks/{trackId}")]
        public ActionResult GetTrack(string trackId) {
            var query = tracksQueryCreator.CreateGetTrackQuery();
            var result = query.Get(trackId);
            return this.BuildResponseOfType<TrackResponse, Track>(result);
        }
        
        [HttpPost("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult AddTrackToPLayList(string playlistId, string trackId) {
            commandQueue.Queue(new AddTrackToPLayList(trackId, playlistId));
            return Ok();
        }

        [HttpDelete("playlists/{playlistId}/tracks/{trackId}")]
        public ActionResult RemoveTrackFromPlayList(string playlistId, string trackId) {
            commandQueue.Queue(new RemoveTrackFromPlayList(trackId, playlistId));
            return Ok();
        }
        
    }
}