using System.Threading.Tasks;
using MyMusic.Application.Write.Ports.Notifications;
using MyMusic.Application.Write.Ports.Websockets;
using MyMusic.Events;

namespace MyMusic.Application.Write.EventHandlers {
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