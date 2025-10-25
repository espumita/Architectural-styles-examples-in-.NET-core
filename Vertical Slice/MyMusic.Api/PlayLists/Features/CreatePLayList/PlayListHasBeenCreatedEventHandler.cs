using System.Threading.Tasks;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.PlayLists.Features.CreatePlayList {
    public class PlayListHasBeenCreatedEventHandler {
        private readonly PlayListNotifier playListNotifier;
        private readonly Websocket websocket;

        public PlayListHasBeenCreatedEventHandler(PlayListNotifier playListNotifier, Websocket websocket) {
            this.playListNotifier = playListNotifier;
            this.websocket = websocket;
        }

        public async Task Handle(PlayListHasBeenCreated @event) {
            playListNotifier.NotifyPlayListHasBeenCreated(@event.PlayListId, @event.PlayListName);
            await websocket.PushMessageWithEventToAll(@event);
        }
    }
}