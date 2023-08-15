using System.Threading.Tasks;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.Tracks.Features.RemoveTrackFromPlayList {
    public class TrackHasBeenRemovedFromPlayListEventHandler {
        private readonly TracksNotifier tracksNotifier;
        private readonly Websocket websocket;

        public TrackHasBeenRemovedFromPlayListEventHandler(TracksNotifier tracksNotifier, Websocket websocket) {
            this.tracksNotifier = tracksNotifier;
            this.websocket = websocket;
        }

        public async Task Handle(TrackHasBeenRemovedFromPlayList @event) {
            tracksNotifier.NotifyTrackHasRemovedFromPlayList(@event.TrackId, @event.PlayListId);
            await websocket.PushMessageWithEventToAll(@event);
        }
    }
}