using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasBeenCreatedEventHandlerTests {
        private PlayListHasBeenCreatedEventHandler playListHasBeenCreated;
        private PlayListNotifierPort playListNotifier;
        private WebsocketPort websocket;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            websocket = Substitute.For<WebsocketPort>();
            playListHasBeenCreated = new PlayListHasBeenCreatedEventHandler(playListNotifier, websocket);
        }

        [Test]
        public void notify_play_list_has_been_created_and_send_to_websocket() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            var @event = new PlayListHasBeenCreated(aPlaylistId, aPlaylistName);

            playListHasBeenCreated.Handle(@event);
            
            playListNotifier.Received().NotifyPlayListHasBeenCreated(aPlaylistId, aPlaylistName);
            websocket.Received().PushMessageWithEvent(@event);
        }
    }
}