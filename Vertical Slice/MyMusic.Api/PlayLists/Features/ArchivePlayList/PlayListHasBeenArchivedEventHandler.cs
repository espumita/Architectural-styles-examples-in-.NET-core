using System.Threading.Tasks;

namespace MyMusic.PlayLists.Features.ArchivePlayList {
    public class PlayListHasBeenArchivedEventHandler {
        private readonly PlayListNotifierPort playListNotifier;
        private readonly WebsocketPort websocket;

        public PlayListHasBeenArchivedEventHandler(PlayListNotifierPort playListNotifier, WebsocketPort websocket) {
            this.playListNotifier = playListNotifier;
            this.websocket = websocket;
        }

        public async Task Handle(PlayListHasBeenArchived @event) {
            playListNotifier.NotifyPlayListHasBeenArchived(@event.PlayListId);
            await websocket.PushMessageWithEventToAll(@event);
        }
    }
}