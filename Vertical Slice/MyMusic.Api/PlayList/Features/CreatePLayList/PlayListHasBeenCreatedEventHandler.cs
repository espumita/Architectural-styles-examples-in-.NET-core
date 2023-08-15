using System.Threading.Tasks;

namespace MyMusic.PlayList.Features.CreatePLayList {
    public class PlayListHasBeenCreatedEventHandler {
        private readonly PlayListNotifierPort playListNotifier;
        private readonly WebsocketPort websocket;

        public PlayListHasBeenCreatedEventHandler(PlayListNotifierPort playListNotifier, WebsocketPort websocket) {
            this.playListNotifier = playListNotifier;
            this.websocket = websocket;
        }

        public async Task Handle(PlayListHasBeenCreated @event) {
            playListNotifier.NotifyPlayListHasBeenCreated(@event.PlayListId, @event.PlayListName);
            await websocket.PushMessageWithEventToAll(@event);
        }
    }
}