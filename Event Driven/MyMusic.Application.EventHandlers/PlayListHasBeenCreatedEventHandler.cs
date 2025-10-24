
using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class PlayListHasBeenCreatedEventHandler {
        private readonly PlayListNotifierPort playListNotifier;

        public PlayListHasBeenCreatedEventHandler(PlayListNotifierPort playListNotifier) {
            this.playListNotifier = playListNotifier;
        }

        public void Handle(PlayListHasBeenCreated @event) {
            playListNotifier.NotifyPlayListHasBeenCreated(@event.PlayListId, @event.PlayListName);
        }
    }
}