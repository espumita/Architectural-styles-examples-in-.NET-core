using System.Threading.Tasks;
using MyMusic.PlayLists.Features;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.Tracks.Features.AddTrackToPLayList {
    public class TrackHasBeenAddedToPlayListEventHandler {
        private readonly TracksNotifier tracksNotifier;
        private readonly Websocket websocket;

        public TrackHasBeenAddedToPlayListEventHandler(TracksNotifier tracksNotifier, Websocket websocket) {
            this.tracksNotifier = tracksNotifier;
            this.websocket = websocket;
        }

        public async Task Handle(TrackHasBeenAddedToPlayList @event) {
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(@event.TrackId, @event.PlayListId);
            await websocket.PushMessageWithEventToAll(@event);
        }
    }
}