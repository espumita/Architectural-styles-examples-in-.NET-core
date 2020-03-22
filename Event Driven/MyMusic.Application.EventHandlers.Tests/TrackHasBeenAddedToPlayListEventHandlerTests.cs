using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class TrackHasBeenAddedToPlayListEventHandlerTests {
        private TrackHasBeenAddedToPlayListEventHandler playListHasImageBeenCreatedEventHandler;
        private TracksNotifierPort tracksNotifier;


        [SetUp]
        public void SetUp() {
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            playListHasImageBeenCreatedEventHandler = new TrackHasBeenAddedToPlayListEventHandler(tracksNotifier);
        }

        [Test]
        public void notify_play_list_image_url_has_changed() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            
            playListHasImageBeenCreatedEventHandler.Handle(new TrackHasBeenAddedToPlayList(aTrackId, aPlaylistId));
            
            tracksNotifier.Received().NotifyTrackHasBeenAddedToPlayList(aTrackId, aPlaylistId);
        }
    }
}