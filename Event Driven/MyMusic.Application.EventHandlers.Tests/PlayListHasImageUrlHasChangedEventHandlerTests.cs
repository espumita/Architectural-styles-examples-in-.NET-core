using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasImageUrlHasChangedEventHandlerTests {
        private PlayListHasImageUrlHasChangedEventHandler playListHasImageUrlHasChanged;
        private PlayListNotifierPort playListNotifier;


        public PlayListHasImageUrlHasChangedEventHandlerTests() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            playListHasImageUrlHasChanged = new PlayListHasImageUrlHasChangedEventHandler(playListNotifier);
        }

        [Fact]
        public void notify_play_list_image_url_has_changed() {
            var aPlaylistId = APlaylist.Id;
            var aNewPlayListImageUrl = APlaylist.ImageUrl;
            
            playListHasImageUrlHasChanged.Handle(new PlayListImageUrlHasChanged(aPlaylistId, aNewPlayListImageUrl));
            
            playListNotifier.Received().NotifyPlayListImageUrlHasChanged(aPlaylistId, aNewPlayListImageUrl);
        }
    }
}