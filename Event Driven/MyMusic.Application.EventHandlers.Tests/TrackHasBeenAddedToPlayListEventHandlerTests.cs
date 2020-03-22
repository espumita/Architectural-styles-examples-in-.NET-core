using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class TrackHasBeenAddedToPlayListEventHandlerTests {
        private TrackHasBeenAddedToPlayListEventHandler trackHasBeenAddedToPlayListEventHandler;
        private TracksNotifierPort tracksNotifier;


        [SetUp]
        public void SetUp() {
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            trackHasBeenAddedToPlayListEventHandler = new TrackHasBeenAddedToPlayListEventHandler(tracksNotifier);
        }

        [Test]
        public void notify_track_has_been_added_to_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            
            trackHasBeenAddedToPlayListEventHandler.Handle(new TrackHasBeenAddedToPlayList(aTrackId, aPlaylistId));
            
            tracksNotifier.Received().NotifyTrackHasBeenAddedToPlayList(aTrackId, aPlaylistId);
        }
    }
}