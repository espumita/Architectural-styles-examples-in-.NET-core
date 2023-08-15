using System.Threading.Tasks;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Features;
using MyMusic.Shared.Infrastructure;
using MyMusic.Tracks.Features;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.Tracks.RemoveTrackFromPlayList {

    public class TrackHasBeenRemovedFromPlayListEventHandlerTests {
        private TrackHasBeenRemovedFromPlayListEventHandler trackHasBeenRemovedFromPlayList;
        private TracksNotifier tracksNotifier;
        private Websocket websocket;


        [SetUp]
        public void SetUp() {
            tracksNotifier = Substitute.For<TracksNotifier>();
            websocket = Substitute.For<Websocket>();
            trackHasBeenRemovedFromPlayList = new TrackHasBeenRemovedFromPlayListEventHandler(tracksNotifier, websocket);
        }

        [Test]
        public async Task notify_track_has_removed_added_to_play_list_and_send_to_websocket() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var @event = new TrackHasBeenRemovedFromPlayList(aTrackId, aPlaylistId);

            await trackHasBeenRemovedFromPlayList.Handle(@event);
            
            tracksNotifier.Received().NotifyTrackHasRemovedFromPlayList(aTrackId, aPlaylistId);
            await websocket.Received().PushMessageWithEventToAll(@event);
        }
    }
}