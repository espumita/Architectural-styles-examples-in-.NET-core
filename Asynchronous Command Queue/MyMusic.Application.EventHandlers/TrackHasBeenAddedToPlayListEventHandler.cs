using System.Threading.Tasks;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class TrackHasBeenAddedToPlayListEventHandler {
        private readonly TracksNotifierPort tracksNotifier;
        private readonly WebsocketPort websocketPort;

        public TrackHasBeenAddedToPlayListEventHandler(TracksNotifierPort tracksNotifier, WebsocketPort websocketPort) {
            this.tracksNotifier = tracksNotifier;
            this.websocketPort = websocketPort;
        }

        public async Task Handle(TrackHasBeenAddedToPlayList @event) {
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(@event.TrackId, @event.PlayListId);
            await websocketPort.PushMessageWithEventToAll(@event);
        }
    }
}