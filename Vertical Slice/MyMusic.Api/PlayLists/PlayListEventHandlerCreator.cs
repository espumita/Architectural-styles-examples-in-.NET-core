using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePLayList;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Websockets;

namespace MyMusic.PlayLists {

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