using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePlayList;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.PlayLists {

    public class PlayListEventHandlerCreator {
        private readonly SignalRWebsocket signalRWebsocket;

        public PlayListEventHandlerCreator(SignalRWebsocket signalRWebsocket) {
            this.signalRWebsocket = signalRWebsocket;
        }

        public PlayListHasBeenCreatedEventHandler PlayListHasBeenCreated() {
            var notifier = new PlayListSpotifyApi();
            return new PlayListHasBeenCreatedEventHandler(notifier, signalRWebsocket);
        }

        public PlayListHasBeenArchivedEventHandler PlayListHasBeenArchived() {
            var notifier = new PlayListSpotifyApi();
            return new PlayListHasBeenArchivedEventHandler(notifier, signalRWebsocket);
        }

        public PlayListHasBeenRenamedEventHandler PlayListHasBeenRenamed() {
            var notifier = new PlayListSpotifyApi();
            return new PlayListHasBeenRenamedEventHandler(notifier, signalRWebsocket);
        }

        public PlayListHasImageUrlHasChangedEventHandler PlayListImageUrlHasChanged() {
            var notifier = new PlayListSpotifyApi();
            return new PlayListHasImageUrlHasChangedEventHandler(notifier, signalRWebsocket);
        }
    }
}