using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasBeenArchivedEventHandlerTests {
        private PlayListHasBeenArchivedEventHandler playListHasBeenArchivedEventHandler;
        private PlayListNotifierPort playListNotifier;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            playListHasBeenArchivedEventHandler = new PlayListHasBeenArchivedEventHandler(playListNotifier);
        }

        [Test]
        public void notify_play_list_has_been_archived() {
            var aPlaylistId = APlaylist.Id;
            
            playListHasBeenArchivedEventHandler.Handle(new PlayListHasBeenArchived(aPlaylistId));
            
            playListNotifier.Received().NotifyPlayListHasBeenArchived(aPlaylistId);
        }
    }
}