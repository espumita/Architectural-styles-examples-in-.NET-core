using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Services;
using MyMusic.Infrastructure.Persistence;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    [Microsoft.AspNetCore.Components.Route("tracks")]
    public class TracksController : Controller {
        
        [HttpGet("{trackId}")]
        public TrackResponse GetTrack(string trackId) {
            var tracksDatabaseAdapter = new TracksDatabaseAdapter();
            var tracksService = new TracksService(tracksDatabaseAdapter);
            var track = tracksService.Get(trackId);
            return TrackResponse.From(track);
        }

    }
}