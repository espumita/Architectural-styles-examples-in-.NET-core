using System.Threading.Tasks;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.Shared.Infrastructure;
using NSubstitute;
using Xunit;

namespace MyMusic.Api.Tests.PlayLists.ChangePlayListImageUrl {

    public class PlayListHasImageUrlHasChangedEventHandlerTests {
        private PlayListHasImageUrlHasChangedEventHandler playListHasImageUrlHasChanged;
        private PlayListNotifier playListNotifier;
        private Websocket websocket;


        public PlayListHasImageUrlHasChangedEventHandlerTests() {
            playListNotifier = Substitute.For<PlayListNotifier>();
            websocket = Substitute.For<Websocket>();
            playListHasImageUrlHasChanged = new PlayListHasImageUrlHasChangedEventHandler(playListNotifier, websocket);
        }

        [Fact]
        public async Task notify_play_list_image_url_has_changed_and_send_to_websocket() {
            var aPlaylistId = APlaylist.Id;
            var aNewPlayListImageUrl = APlaylist.ImageUrl;
            var @event = new PlayListImageUrlHasChanged(aPlaylistId, aNewPlayListImageUrl);

            await playListHasImageUrlHasChanged.Handle(@event);
            
            playListNotifier.Received().NotifyPlayListImageUrlHasChanged(aPlaylistId, aNewPlayListImageUrl);
            playListNotifier.Received().NotifyPlayListImageUrlHasChanged(aPlaylistId, aNewPlayListImageUrl);
        }
    }
}