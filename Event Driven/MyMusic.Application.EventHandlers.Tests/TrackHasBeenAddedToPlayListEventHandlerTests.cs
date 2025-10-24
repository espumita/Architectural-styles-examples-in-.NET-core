using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.EventHandlers.Tests {

    public class TrackHasBeenAddedToPlayListEventHandlerTests {
        private TrackHasBeenAddedToPlayListEventHandler trackHasBeenAddedToPlayList;
        private TracksNotifierPort tracksNotifier;


        public TrackHasBeenAddedToPlayListEventHandlerTests() {
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            trackHasBeenAddedToPlayList = new TrackHasBeenAddedToPlayListEventHandler(tracksNotifier);
        }

        [Fact]
        public void notify_track_has_been_added_to_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            
            trackHasBeenAddedToPlayList.Handle(new TrackHasBeenAddedToPlayList(aTrackId, aPlaylistId));
            
            tracksNotifier.Received().NotifyTrackHasBeenAddedToPlayList(aTrackId, aPlaylistId);
        }
    }
}