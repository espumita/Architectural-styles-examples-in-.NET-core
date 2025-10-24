using System.Threading.Tasks;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasBeenCreatedEventHandlerTests {
        private PlayListHasBeenCreatedEventHandler playListHasBeenCreated;
        private PlayListNotifierPort playListNotifier;
        private WebsocketPort websocket;


        public PlayListHasBeenCreatedEventHandlerTests() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            websocket = Substitute.For<WebsocketPort>();
            playListHasBeenCreated = new PlayListHasBeenCreatedEventHandler(playListNotifier, websocket);
        }

        [Fact]
        public async Task notify_play_list_has_been_created_and_send_to_websocket() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            var @event = new PlayListHasBeenCreated(aPlaylistId, aPlaylistName);

            await playListHasBeenCreated.Handle(@event);
            
            playListNotifier.Received().NotifyPlayListHasBeenCreated(aPlaylistId, aPlaylistName);
            await websocket.Received().PushMessageWithEventToAll(@event);
        }
    }
}