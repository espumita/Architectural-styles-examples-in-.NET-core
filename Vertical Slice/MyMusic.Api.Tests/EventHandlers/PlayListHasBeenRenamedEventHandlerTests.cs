using System.Threading.Tasks;
using MyMusic.Application.Write.EventHandlers;
using MyMusic.Application.Write.Ports.Notifications;
using MyMusic.Application.Write.Ports.Websockets;
using MyMusic.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.EventHandlers {

    public class PlayListHasBeenRenamedEventHandlerTests {
        private PlayListHasBeenRenamedEventHandler playListHasBeenRenamed;
        private PlayListNotifierPort playListNotifier;
        private WebsocketPort websocketPort;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            websocketPort = Substitute.For<WebsocketPort>();
            playListHasBeenRenamed = new PlayListHasBeenRenamedEventHandler(playListNotifier, websocketPort);
        }

        [Test]
        public async Task notify_play_list_has_been_renamed_and_send_to_websocket() {
            var aPlaylistId = APlaylist.Id;
            var aNewPlaylistName = APlaylist.Name;
            var @event = new PlayListHasBeenRenamed(aPlaylistId, aNewPlaylistName);

            await playListHasBeenRenamed.Handle(@event);
            
            playListNotifier.Received().NotifyPlayListHasBeenRenamed(aPlaylistId, aNewPlaylistName);
            await websocketPort.Received().PushMessageWithEventToAll(@event);
        }
    }
}