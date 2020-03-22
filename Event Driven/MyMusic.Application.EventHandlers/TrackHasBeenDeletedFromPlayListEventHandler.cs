using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class TrackHasBeenDeletedFromPlayListEventHandler {
        private readonly TracksNotifierPort tracksNotifier;

        public TrackHasBeenDeletedFromPlayListEventHandler(TracksNotifierPort tracksNotifier) {
            this.tracksNotifier = tracksNotifier;
        }

        public void Handle(TrackHasBeenDeletedFromPlayList @event) {
            tracksNotifier.NotifyTrackHasRemovedFromPlayList(@event.TrackId, @event.PlayListId);
        }
    }
}