using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class PlayListHasBeenRenamedEventHandler {
        private readonly PlayListNotifierPort playListNotifier;

        public PlayListHasBeenRenamedEventHandler(PlayListNotifierPort playListNotifier) {
            this.playListNotifier = playListNotifier;
        }

        public void Handle(PlayListHasBeenRenamed @event) {
            playListNotifier.NotifyPlayListHasBeenRenamed(@event.PlayListId, @event.NewPlayListName);
        }
    }
}