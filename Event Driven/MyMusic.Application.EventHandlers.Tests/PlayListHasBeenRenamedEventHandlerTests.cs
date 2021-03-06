using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasBeenRenamedEventHandlerTests {
        private PlayListHasBeenRenamedEventHandler playListHasBeenRenamed;
        private PlayListNotifierPort playListNotifier;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            playListHasBeenRenamed = new PlayListHasBeenRenamedEventHandler(playListNotifier);
        }

        [Test]
        public void notify_play_list_has_been_renamed() {
            var aPlaylistId = APlaylist.Id;
            var aNewPlaylistName = APlaylist.Name;
            
            playListHasBeenRenamed.Handle(new PlayListHasBeenRenamed(aPlaylistId, aNewPlaylistName));
            
            playListNotifier.Received().NotifyPlayListHasBeenRenamed(aPlaylistId, aNewPlaylistName);
        }
    }
}