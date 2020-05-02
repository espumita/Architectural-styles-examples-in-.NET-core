using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class PlayListHasBeenCreatedEventHandlerTests {
        private PlayListHasBeenCreatedEventHandler playListHasBeenCreated;
        private PlayListNotifierPort playListNotifier;


        [SetUp]
        public void SetUp() {
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            playListHasBeenCreated = new PlayListHasBeenCreatedEventHandler(playListNotifier);
        }

        [Test]
        public void notify_play_list_has_been_created() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            
            playListHasBeenCreated.Handle(new PlayListHasBeenCreated(aPlaylistId, aPlaylistName));
            
            playListNotifier.Received().NotifyPlayListHasBeenCreated(aPlaylistId, aPlaylistName);
        }
    }
}