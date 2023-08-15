using System.Threading.Tasks;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.CreatePLayList;
using MyMusic.Shared.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.PlayLists.CreatePLayList {

    public class PlayListHasBeenCreatedEventHandlerTests {
        private PlayListHasBeenCreatedEventHandler playListHasBeenCreated;
        private PlayListNotifier playListNotifier;
        private Websocket websocket;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifier>();
            websocket = Substitute.For<Websocket>();
            playListHasBeenCreated = new PlayListHasBeenCreatedEventHandler(playListNotifier, websocket);
        }

        [Test]
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