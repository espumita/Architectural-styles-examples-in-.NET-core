using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasBeenArchivedEventHandlerTests {
        private PlayListHasBeenArchivedEventHandler playListHasBeenArchived;
        private PlayListNotifierPort playListNotifier;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            playListHasBeenArchived = new PlayListHasBeenArchivedEventHandler(playListNotifier);
        }

        [Test]
        public void notify_play_list_has_been_archived() {
            var aPlaylistId = APlaylist.Id;
            
            playListHasBeenArchived.Handle(new PlayListHasBeenArchived(aPlaylistId));
            
            playListNotifier.Received().NotifyPlayListHasBeenArchived(aPlaylistId);
        }
    }
}