using System.Threading.Tasks;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
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