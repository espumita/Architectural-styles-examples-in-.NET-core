using MyMusic.Shared.Websockets;
using MyMusic.Tracks.Features;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.Tracks {
    public class TrackEventHandlerCreator {
        private readonly SignalRWebsocketAdapter signalRWebsocketAdapter;

        public TrackEventHandlerCreator(SignalRWebsocketAdapter signalRWebsocketAdapter) {
            this.signalRWebsocketAdapter = signalRWebsocketAdapter;
        }

        public TrackHasBeenAddedToPlayListEventHandler TrackHasBeenAddedToPlayList() {
            var notifier = new TraksSpotifyApiAdapter();
            return new TrackHasBeenAddedToPlayListEventHandler(notifier, signalRWebsocketAdapter);
        }

        public TrackHasBeenRemovedFromPlayListEventHandler TrackHasBeenRemovedFromToPlayList() {
            var notifier = new TraksSpotifyApiAdapter();
            return new TrackHasBeenRemovedFromPlayListEventHandler(notifier, signalRWebsocketAdapter);
        }
    }
}