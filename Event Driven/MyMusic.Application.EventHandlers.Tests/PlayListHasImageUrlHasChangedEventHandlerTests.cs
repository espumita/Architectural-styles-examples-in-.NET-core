using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasImageUrlHasChangedEventHandlerTests {
        private PlayListHasImageUrlHasChangedEventHandler playListHasImageUrlHasChanged;
        private PlayListNotifierPort playListNotifier;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            playListHasImageUrlHasChanged = new PlayListHasImageUrlHasChangedEventHandler(playListNotifier);
        }

        [Test]
        public void notify_play_list_image_url_has_changed() {
            var aPlaylistId = APlaylist.Id;
            var aNewPlayListImageUrl = APlaylist.ImageUrl;
            
            playListHasImageUrlHasChanged.Handle(new PlayListImageUrlHasChanged(aPlaylistId, aNewPlayListImageUrl));
            
            playListNotifier.Received().NotifyPlayListImageUrlHasChanged(aPlaylistId, aNewPlayListImageUrl);
        }
    }
}