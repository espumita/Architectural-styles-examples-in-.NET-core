using System.Threading.Tasks;
using MyMusic.PlayLists.Features;

namespace MyMusic.Tracks.Features.RemoveTrackFromPlayList {
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