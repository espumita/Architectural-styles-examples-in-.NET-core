using System.Threading.Tasks;
using MyMusic.PlayList.Features;

namespace MyMusic.Tracks.Features.AddTrackToPLayList {
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