using System.Threading.Tasks;
using MyMusic.Application.Write.Ports.Notifications;
using MyMusic.Application.Write.Ports.Websockets;
using MyMusic.Events;

namespace MyMusic.Application.Write.EventHandlers {
    public class PlayListHasImageUrlHasChangedEventHandler {
        private readonly PlayListNotifierPort playListNotifier;
        private readonly WebsocketPort websocketPort;

        public PlayListHasImageUrlHasChangedEventHandler(PlayListNotifierPort playListNotifier, WebsocketPort websocketPort) {
            this.playListNotifier = playListNotifier;
            this.websocketPort = websocketPort;
        }

        public async Task Handle(PlayListImageUrlHasChanged @event) {
            playListNotifier.NotifyPlayListImageUrlHasChanged(@event.PlayListId, @event.ImageUrl);
            await websocketPort.PushMessageWithEventToAll(@event);
        }
    }
}