using System.Threading.Tasks;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasBeenArchivedEventHandlerTests {
        private PlayListHasBeenArchivedEventHandler playListHasBeenArchived;
        private PlayListNotifierPort playListNotifier;
        private WebsocketPort websocket;


        public PlayListHasBeenArchivedEventHandlerTests() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            websocket = Substitute.For<WebsocketPort>();
            playListHasBeenArchived = new PlayListHasBeenArchivedEventHandler(playListNotifier, websocket);
        }

        [Fact]
        public async Task notify_play_list_has_been_archived_and_send_to_websocket() {
            var aPlaylistId = APlaylist.Id;
            var @event = new PlayListHasBeenArchived(aPlaylistId);

            await playListHasBeenArchived.Handle(@event);
            
            playListNotifier.Received().NotifyPlayListHasBeenArchived(aPlaylistId);
            await websocket.Received().PushMessageWithEventToAll(@event);
        }
    }
}