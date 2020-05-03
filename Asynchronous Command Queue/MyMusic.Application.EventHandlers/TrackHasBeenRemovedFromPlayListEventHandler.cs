using System.Threading.Tasks;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class TrackHasBeenRemovedFromPlayListEventHandler {
        private readonly TracksNotifierPort tracksNotifier;
        private readonly WebsocketPort websocketPort;

        public TrackHasBeenRemovedFromPlayListEventHandler(TracksNotifierPort tracksNotifier, WebsocketPort websocketPort) {
            this.tracksNotifier = tracksNotifier;
            this.websocketPort = websocketPort;
        }

        public async Task Handle(TrackHasBeenRemovedFromPlayList @event) {
            tracksNotifier.NotifyTrackHasRemovedFromPlayList(@event.TrackId, @event.PlayListId);
            await websocketPort.PushMessageWithEventToAll(@event);
        }
    }
}