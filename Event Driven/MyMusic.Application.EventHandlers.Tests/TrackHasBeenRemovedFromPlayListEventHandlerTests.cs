using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.EventHandlers.Tests {

    public class TrackHasBeenRemovedFromPlayListEventHandlerTests {
        private TrackHasBeenRemovedFromPlayListEventHandler trackHasBeenRemovedFromPlayList;
        private TracksNotifierPort tracksNotifier;


        [SetUp]
        public void SetUp() {
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            trackHasBeenRemovedFromPlayList = new TrackHasBeenRemovedFromPlayListEventHandler(tracksNotifier);
        }

        [Test]
        public void notify_track_has_removed_added_to_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            
            trackHasBeenRemovedFromPlayList.Handle(new TrackHasBeenRemovedFromPlayList(aTrackId, aPlaylistId));
            
            tracksNotifier.Received().NotifyTrackHasRemovedFromPlayList(aTrackId, aPlaylistId);
        }
    }
}