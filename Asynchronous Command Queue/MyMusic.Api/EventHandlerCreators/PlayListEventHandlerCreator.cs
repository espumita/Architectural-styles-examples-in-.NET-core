
using MyMusic.Application.EventHandlers;
using MyMusic.Infrastructure.Adapters.Http;
using MyMusic.Infrastructure.Adapters.Websockets;

namespace MyMusic.EventHandlerCreators {

    public class PlayListEventHandlerCreator {
        public PlayListHasBeenCreatedEventHandler PlayListHasBeenCreated() {
            var notifier = new PlayListSpotifyApiAdapter();
            var signalRWebsocketAdapter = new SignalRWebsocketAdapter();
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