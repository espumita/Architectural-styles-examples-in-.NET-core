using System.Threading.Tasks;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Infrastructure;
using NSubstitute;
using Xunit;

namespace MyMusic.Api.Tests.PlayLists.RenamePlaylist {

    public class PlayListHasBeenRenamedEventHandlerTests {
        private PlayListHasBeenRenamedEventHandler playListHasBeenRenamed;
        private PlayListNotifier playListNotifier;
        private Websocket websocket;


        public PlayListHasBeenRenamedEventHandlerTests() {
            playListNotifier = Substitute.For<PlayListNotifier>();
            websocket = Substitute.For<Websocket>();
            playListHasBeenRenamed = new PlayListHasBeenRenamedEventHandler(playListNotifier, websocket);
        }

        [Fact]
        public async Task notify_play_list_has_been_renamed_and_send_to_websocket() {
            var aPlaylistId = APlaylist.Id;
            var aNewPlaylistName = APlaylist.Name;
            var @event = new PlayListHasBeenRenamed(aPlaylistId, aNewPlaylistName);

            await playListHasBeenRenamed.Handle(@event);
            
            playListNotifier.Received().NotifyPlayListHasBeenRenamed(aPlaylistId, aNewPlaylistName);
            await websocket.Received().PushMessageWithEventToAll(@event);
        }
    }
}