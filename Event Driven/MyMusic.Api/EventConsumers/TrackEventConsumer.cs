using MyMusic.Domain.Events;
using MyMusic.EventHandlerCreators;

namespace MyMusic.EventConsumers {

    public class TrackEventConsumer {
        private readonly TrackEventHandlerCreator trackEventHandlerCreator;

        public TrackEventConsumer(TrackEventHandlerCreator trackEventHandlerCreator) {
            this.trackEventHandlerCreator = trackEventHandlerCreator;
        }

        public void Consume(TrackHasBeenAddedToPlayList @event) {
            var trackHasBeenAddedToPlayListEventHandler = trackEventHandlerCreator.TrackHasBeenAddedToPlayList();
            trackHasBeenAddedToPlayListEventHandler.Handle(@event);
        }

        public void Consume(TrackHasBeenDeletedFromPlayList @event) {
            var trackHasBeenDeletedFromPlayListEventHandler = trackEventHandlerCreator.TrackHasBeenRemovedFromToPlayList();
            trackHasBeenDeletedFromPlayListEventHandler.Handle(@event);
        }
    }
}