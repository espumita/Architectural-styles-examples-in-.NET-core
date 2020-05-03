using MyMusic.Domain.Events;
using MyMusic.EventHandlerCreators;

namespace MyMusic.EventConsumers {
    public class PlayListEventConsumer {
        private readonly PlayListEventHandlerCreator playListEventHandlerCreator;

        public PlayListEventConsumer(PlayListEventHandlerCreator playListEventHandlerCreator) {
            this.playListEventHandlerCreator = playListEventHandlerCreator;
        }

        public async void Consume(PlayListHasBeenCreated @event) {
            var playListHasBeenCreatedEventHandler = playListEventHandlerCreator.PlayListHasBeenCreated();
            await playListHasBeenCreatedEventHandler.Handle(@event);
        }

        public async void Consume(PlayListHasBeenArchived @event) {
            var playListHasBeenArchivedEventHandler = playListEventHandlerCreator.PlayListHasBeenArchived();
            await playListHasBeenArchivedEventHandler.Handle(@event);
        }

        public async void Consume(PlayListHasBeenRenamed @event) {
            var playListHasBeenRenamedEventHandler = playListEventHandlerCreator.PlayListHasBeenRenamed();
            await playListHasBeenRenamedEventHandler.Handle(@event);
        }

        public async void Consume(PlayListImageUrlHasChanged @event) {
            var playListHasBeenRenamedEventHandler = playListEventHandlerCreator.PlayListImageUrlHasChanged();
            await playListHasBeenRenamedEventHandler.Handle(@event);
        }
    }
}