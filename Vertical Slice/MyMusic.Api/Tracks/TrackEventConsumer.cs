using MyMusic.Tracks.Features.AddTrackToPLayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.Tracks {

    public class TrackEventConsumer {
        private readonly TrackEventHandlerCreator trackEventHandlerCreator;

        public TrackEventConsumer(TrackEventHandlerCreator trackEventHandlerCreator) {
            this.trackEventHandlerCreator = trackEventHandlerCreator;
        }

        public async void Consume(TrackHasBeenAddedToPlayList @event) {
            var trackHasBeenAddedToPlayListEventHandler = trackEventHandlerCreator.TrackHasBeenAddedToPlayList();
            await trackHasBeenAddedToPlayListEventHandler.Handle(@event);
        }

        public async void Consume(TrackHasBeenRemovedFromPlayList @event) {
            var trackHasBeenRemovedFromPlayListEventHandler = trackEventHandlerCreator.TrackHasBeenRemovedFromToPlayList();
            await trackHasBeenRemovedFromPlayListEventHandler.Handle(@event);
        }
    }
}