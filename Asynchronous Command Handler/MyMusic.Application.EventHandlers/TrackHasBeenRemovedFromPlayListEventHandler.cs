using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class TrackHasBeenRemovedFromPlayListEventHandler {
        private readonly TracksNotifierPort tracksNotifier;

        public TrackHasBeenRemovedFromPlayListEventHandler(TracksNotifierPort tracksNotifier) {
            this.tracksNotifier = tracksNotifier;
        }

        public void Handle(TrackHasBeenRemovedFromPlayList @event) {
            tracksNotifier.NotifyTrackHasRemovedFromPlayList(@event.TrackId, @event.PlayListId);
        }
    }
}