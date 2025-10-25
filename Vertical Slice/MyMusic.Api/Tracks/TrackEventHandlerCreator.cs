using MyMusic.Shared.Infrastructure;
using MyMusic.Tracks.Features;
using MyMusic.Tracks.Features.AddTrackToPlayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.Tracks {
    public class TrackEventHandlerCreator {
        private readonly SignalRWebsocket signalRWebsocket;

        public TrackEventHandlerCreator(SignalRWebsocket signalRWebsocket) {
            this.signalRWebsocket = signalRWebsocket;
        }

        public TrackHasBeenAddedToPlayListEventHandler TrackHasBeenAddedToPlayList() {
            var notifier = new TraksSpotifyApi();
            return new TrackHasBeenAddedToPlayListEventHandler(notifier, signalRWebsocket);
        }

        public TrackHasBeenRemovedFromPlayListEventHandler TrackHasBeenRemovedFromToPlayList() {
            var notifier = new TraksSpotifyApi();
            return new TrackHasBeenRemovedFromPlayListEventHandler(notifier, signalRWebsocket);
        }
    }
}