using System.Threading.Tasks;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.PlayLists.Features.ChangePlayListImageUrl {
    public class PlayListHasImageUrlHasChangedEventHandler {
        private readonly PlayListNotifier playListNotifier;
        private readonly Websocket websocket;

        public PlayListHasImageUrlHasChangedEventHandler(PlayListNotifier playListNotifier, Websocket websocket) {
            this.playListNotifier = playListNotifier;
            this.websocket = websocket;
        }

        public async Task Handle(PlayListImageUrlHasChanged @event) {
            playListNotifier.NotifyPlayListImageUrlHasChanged(@event.PlayListId, @event.ImageUrl);
            await websocket.PushMessageWithEventToAll(@event);
        }
    }
}