using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class TrackHasBeenAddedToPlayListEventHandler {
        private readonly TracksNotifierPort tracksNotifier;

        public TrackHasBeenAddedToPlayListEventHandler(TracksNotifierPort tracksNotifier) {
            this.tracksNotifier = tracksNotifier;
        }

        public void Handle(TrackHasBeenAddedToPlayList @event) {
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(@event.TrackId, @event.PlayListId);
        }
    }
}