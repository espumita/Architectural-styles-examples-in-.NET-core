using MyMusic.Domain.Events;
using MyMusic.EventHandlerCreators;

namespace MyMusic.EventConsumers {
    public class PlayListHasBeenCreatedEventConsumer {
        private readonly PlayListEventHandlerCreator playListEventHandlerCreator;

        public PlayListHasBeenCreatedEventConsumer(PlayListEventHandlerCreator playListEventHandlerCreator) {
            this.playListEventHandlerCreator = playListEventHandlerCreator;
        }

        public void Consume(PlayListHasBeenCreated @event) {
            var playListHasBeenCreatedEventHandler = playListEventHandlerCreator.PlayListHasBeenCreated();
            playListHasBeenCreatedEventHandler.Handle(@event);
        }

        public void Consume(PlayListHasBeenArchived @event) {
            var playListHasBeenArchivedEventHandler = playListEventHandlerCreator.PlayListHasBeenArchived();
            playListHasBeenArchivedEventHandler.Handle(@event);
        }

        public void Consume(PlayListHasBeenRenamed @event) {
            var playListHasBeenRenamedEventHandler = playListEventHandlerCreator.PlayListHasBeenRenamed();
            playListHasBeenRenamedEventHandler.Handle(@event);
        }

        public void Consume(PlayListImageUrlHasChanged @event) {
            var playListHasBeenRenamedEventHandler = playListEventHandlerCreator.PlayListImageUrlHasChanged();
            playListHasBeenRenamedEventHandler.Handle(@event);
        }
    }
}