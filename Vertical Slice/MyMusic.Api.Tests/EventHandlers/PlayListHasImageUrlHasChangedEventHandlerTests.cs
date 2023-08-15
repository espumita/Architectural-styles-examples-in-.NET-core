using System.Threading.Tasks;
using MyMusic.PlayList.Features;
using MyMusic.PlayList.Features.ChangePlayListImageUrl;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.EventHandlers {

    public class PlayListHasImageUrlHasChangedEventHandlerTests {
        private PlayListHasImageUrlHasChangedEventHandler playListHasImageUrlHasChanged;
        private PlayListNotifierPort playListNotifier;
        private WebsocketPort websocketPort;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            websocketPort = Substitute.For<WebsocketPort>();
            playListHasImageUrlHasChanged = new PlayListHasImageUrlHasChangedEventHandler(playListNotifier, websocketPort);
        }

        [Test]
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