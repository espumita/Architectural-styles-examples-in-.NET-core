using MyMusic.Application.EventHandlers;
using MyMusic.Infrastructure.Adapters.Http;

namespace MyMusic.EventHandlerCreators {
    public class TrackEventHandlerCreator {
        public TrackHasBeenAddedToPlayListEventHandler TrackHasBeenAddedToPlayList() {
            var traksSpotifyApiAdapter = new TraksSpotifyApiAdapter();
            return new TrackHasBeenAddedToPlayListEventHandler(traksSpotifyApiAdapter);
        }
    }
}