using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.EventHandlers.Tests {

    public class TrackHasBeenRemovedFromPlayListEventHandlerTests {
        private TrackHasBeenRemovedFromPlayListEventHandler trackHasBeenRemovedFromPlayList;
        private TracksNotifierPort tracksNotifier;


        public TrackHasBeenRemovedFromPlayListEventHandlerTests() {
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            trackHasBeenRemovedFromPlayList = new TrackHasBeenRemovedFromPlayListEventHandler(tracksNotifier);
        }

        [Fact]
        public void notify_track_has_removed_added_to_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            
            trackHasBeenRemovedFromPlayList.Handle(new TrackHasBeenRemovedFromPlayList(aTrackId, aPlaylistId));
            
            tracksNotifier.Received().NotifyTrackHasRemovedFromPlayList(aTrackId, aPlaylistId);
        }
    }
}