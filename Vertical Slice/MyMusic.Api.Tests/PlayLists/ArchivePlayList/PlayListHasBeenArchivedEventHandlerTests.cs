using System.Threading.Tasks;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.Shared.Infrastructure;
using NSubstitute;
using Xunit;

namespace MyMusic.Api.Tests.PlayLists.ArchivePlayList {

    public class PlayListHasBeenArchivedEventHandlerTests {
        private PlayListHasBeenArchivedEventHandler playListHasBeenArchived;
        private PlayListNotifier playListNotifier;
        private Websocket websocket;


        public PlayListHasBeenArchivedEventHandlerTests() {
            playListNotifier = Substitute.For<PlayListNotifier>();
            websocket = Substitute.For<Websocket>();
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