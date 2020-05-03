using MyMusic.Domain.Events;
using MyMusic.EventHandlerCreators;

namespace MyMusic.EventConsumers {

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