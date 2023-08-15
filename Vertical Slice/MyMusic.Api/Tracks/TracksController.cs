using Microsoft.AspNetCore.Mvc;
using MyMusic.PlayLists.Features.GetPlayListQuery;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using MyMusic.Tracks.Features.GetTrack;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.Tracks {
    
    public class TracksController : Controller {
        
        private readonly CommandQueue commandQueue;
        private readonly TracksQueryCreator tracksQueryCreator;

        public TracksController(CommandQueue commandQueue, TracksQueryCreator tracksQueryCreator) {
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