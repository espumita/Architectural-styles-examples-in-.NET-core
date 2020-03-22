using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;

namespace MyMusic.Application.EventHandlers {
    public class PlayListHasImageUrlHasChangedEventHandler {
        private readonly PlayListNotifierPort playListNotifier;

        public PlayListHasImageUrlHasChangedEventHandler(PlayListNotifierPort playListNotifier) {
            this.playListNotifier = playListNotifier;
        }

        public void Handle(PlayListImageUrlHasChanged @event) {
            playListNotifier.NotifyPlayListImageUrlHasChanged(@event.PlayListId, @event.ImageUrl);
        }
    }
}