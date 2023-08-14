using System.Threading.Tasks;
using MyMusic.Application.Write.Ports.Notifications;
using MyMusic.Application.Write.Ports.Websockets;
using MyMusic.Events;

namespace MyMusic.Application.Write.EventHandlers {
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