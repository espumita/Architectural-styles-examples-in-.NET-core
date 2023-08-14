using System.Threading.Tasks;
using MyMusic.Application.Write.Ports.Notifications;
using MyMusic.Application.Write.Ports.Websockets;
using MyMusic.Events;

namespace MyMusic.Application.Write.EventHandlers {
    public class PlayListHasBeenRenamedEventHandler {
        private readonly PlayListNotifierPort playListNotifier;
        private readonly WebsocketPort websocketPort;

        public PlayListHasBeenRenamedEventHandler(PlayListNotifierPort playListNotifier, WebsocketPort websocketPort) {
            this.playListNotifier = playListNotifier;
            this.websocketPort = websocketPort;
        }

        public async Task Handle(PlayListHasBeenRenamed @event) {
            playListNotifier.NotifyPlayListHasBeenRenamed(@event.PlayListId, @event.NewPlayListName);
            await websocketPort.PushMessageWithEventToAll(@event);
        }
    }
}