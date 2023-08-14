using MyMusic.Application.Write.EventHandlers;
using MyMusic.Infrastructure.Http;
using MyMusic.Websockets;

namespace MyMusic.EventHandlerCreators {
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