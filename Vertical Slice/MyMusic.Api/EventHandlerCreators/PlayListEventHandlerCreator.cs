using MyMusic.Application.Write.EventHandlers;
using MyMusic.Infrastructure.Http;
using MyMusic.Websockets;

namespace MyMusic.EventHandlerCreators {

    public class PlayListEventHandlerCreator {
        private readonly SignalRWebsocketAdapter signalRWebsocketAdapter;

        public PlayListEventHandlerCreator(SignalRWebsocketAdapter signalRWebsocketAdapter) {
            this.signalRWebsocketAdapter = signalRWebsocketAdapter;
        }

        public PlayListHasBeenCreatedEventHandler PlayListHasBeenCreated() {
            var notifier = new PlayListSpotifyApiAdapter();
            return new PlayListHasBeenCreatedEventHandler(notifier, signalRWebsocketAdapter);
        }

        public PlayListHasBeenArchivedEventHandler PlayListHasBeenArchived() {
            var notifier = new PlayListSpotifyApiAdapter();
            return new PlayListHasBeenArchivedEventHandler(notifier, signalRWebsocketAdapter);
        }

        public PlayListHasBeenRenamedEventHandler PlayListHasBeenRenamed() {
            var notifier = new PlayListSpotifyApiAdapter();
            return new PlayListHasBeenRenamedEventHandler(notifier, signalRWebsocketAdapter);
        }

        public PlayListHasImageUrlHasChangedEventHandler PlayListImageUrlHasChanged() {
            var notifier = new PlayListSpotifyApiAdapter();
            return new PlayListHasImageUrlHasChangedEventHandler(notifier, signalRWebsocketAdapter);
        }
    }
}