
using MyMusic.Application.EventHandlers;
using MyMusic.Infrastructure.Adapters.Http;

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
            return new PlayListHasBeenArchivedEventHandler(notifier);
        }

        public PlayListHasBeenRenamedEventHandler PlayListHasBeenRenamed() {
            var notifier = new PlayListSpotifyApiAdapter();
            return new PlayListHasBeenRenamedEventHandler(notifier);
        }

        public PlayListHasImageUrlHasChangedEventHandler PlayListImageUrlHasChanged() {
            var notifier = new PlayListSpotifyApiAdapter();
            return new PlayListHasImageUrlHasChangedEventHandler(notifier);
        }
    }
}