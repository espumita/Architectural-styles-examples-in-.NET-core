using System.Threading.Tasks;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.PlayLists.Features.ArchivePlayList {
    public class PlayListHasBeenArchivedEventHandler {
        private readonly PlayListNotifier playListNotifier;
        private readonly Websocket websocket;

        public PlayListHasBeenArchivedEventHandler(PlayListNotifier playListNotifier, Websocket websocket) {
            this.playListNotifier = playListNotifier;
            this.websocket = websocket;
        }

        public async Task Handle(PlayListHasBeenArchived @event) {
            playListNotifier.NotifyPlayListHasBeenArchived(@event.PlayListId);
            await websocket.PushMessageWithEventToAll(@event);
        }
    }
}