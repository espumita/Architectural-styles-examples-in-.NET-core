using System.Threading.Tasks;
using MyMusic.PlayList.Features;
using MyMusic.PlayList.Features.ArchivePlayList;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.EventHandlers {

    public class PlayListHasBeenArchivedEventHandlerTests {
        private PlayListHasBeenArchivedEventHandler playListHasBeenArchived;
        private PlayListNotifierPort playListNotifier;
        private WebsocketPort websocket;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            websocket = Substitute.For<WebsocketPort>();
            playListHasBeenArchived = new PlayListHasBeenArchivedEventHandler(playListNotifier, websocket);
        }

        [Test]
        public async Task notify_play_list_has_been_archived_and_send_to_websocket() {
            var aPlaylistId = APlaylist.Id;
            var @event = new PlayListHasBeenArchived(aPlaylistId);

            await playListHasBeenArchived.Handle(@event);
            
            playListNotifier.Received().NotifyPlayListHasBeenArchived(aPlaylistId);
            await websocket.Received().PushMessageWithEventToAll(@event);
        }
    }
}