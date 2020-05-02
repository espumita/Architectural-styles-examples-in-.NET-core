using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class PlayListHasBeenArchivedEventHandler {
        private readonly PlayListNotifierPort playListNotifier;

        public PlayListHasBeenArchivedEventHandler(PlayListNotifierPort playListNotifier) {
            this.playListNotifier = playListNotifier;
        }

        public void Handle(PlayListHasBeenArchived @event) {
            playListNotifier.NotifyPlayListHasBeenArchived(@event.PlayListId);
        }
    }
}