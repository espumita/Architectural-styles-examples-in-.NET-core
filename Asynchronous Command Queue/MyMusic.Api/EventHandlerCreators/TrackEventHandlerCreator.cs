using MyMusic.Application.EventHandlers;
using MyMusic.Infrastructure.Adapters.Http;

namespace MyMusic.EventHandlerCreators {
    public class TrackEventHandlerCreator {
        public TrackHasBeenAddedToPlayListEventHandler TrackHasBeenAddedToPlayList() {
            var notifier = new TraksSpotifyApiAdapter();
            return new TrackHasBeenAddedToPlayListEventHandler(notifier);
        }

        public TrackHasBeenRemovedFromPlayListEventHandler TrackHasBeenRemovedFromToPlayList() {
            var notifier = new TraksSpotifyApiAdapter();
            return new TrackHasBeenRemovedFromPlayListEventHandler(notifier);
        }
    }
}