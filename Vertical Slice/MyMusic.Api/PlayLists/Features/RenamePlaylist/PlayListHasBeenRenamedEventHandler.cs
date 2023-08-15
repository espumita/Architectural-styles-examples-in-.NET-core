using System.Threading.Tasks;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.PlayLists.Features.RenamePlaylist {
    public class PlayListHasBeenRenamedEventHandler {
        private readonly PlayListNotifier playListNotifier;
        private readonly Websocket websocket;

        public PlayListHasBeenRenamedEventHandler(PlayListNotifier playListNotifier, Websocket websocket) {
            this.playListNotifier = playListNotifier;
            this.websocket = websocket;
        }

        public async Task Handle(PlayListHasBeenRenamed @event) {
            playListNotifier.NotifyPlayListHasBeenRenamed(@event.PlayListId, @event.NewPlayListName);
            await websocket.PushMessageWithEventToAll(@event);
        }
    }
}