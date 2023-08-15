using MyMusic.PlayList.Features;
using MyMusic.PlayList.Features.ArchivePlayList;
using MyMusic.PlayList.Features.ChangePlayListImageUrl;
using MyMusic.PlayList.Features.CreatePLayList;
using MyMusic.PlayList.Features.RenamePlaylist;
using MyMusic.Shared.Websockets;

namespace MyMusic.PlayList {

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