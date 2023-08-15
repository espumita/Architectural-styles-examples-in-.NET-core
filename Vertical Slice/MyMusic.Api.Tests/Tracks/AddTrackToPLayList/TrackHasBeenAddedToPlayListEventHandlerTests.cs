using System.Threading.Tasks;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Features;
using MyMusic.Shared.Infrastructure;
using MyMusic.Tracks.Features;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.Tracks.AddTrackToPLayList {

    public class TrackHasBeenAddedToPlayListEventHandlerTests {
        private TrackHasBeenAddedToPlayListEventHandler trackHasBeenAddedToPlayList;
        private TracksNotifier tracksNotifier;
        private Websocket websocket;


        [SetUp]
        public void SetUp() {
            tracksNotifier = Substitute.For<TracksNotifier>();
            websocket = Substitute.For<Websocket>();
            trackHasBeenAddedToPlayList = new TrackHasBeenAddedToPlayListEventHandler(tracksNotifier, websocket);
        }

        [Test]
        public async Task notify_track_has_been_added_to_play_list_and_send_to_websocket() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var @event = new TrackHasBeenAddedToPlayList(aTrackId, aPlaylistId);

            await trackHasBeenAddedToPlayList.Handle(@event);
            
            tracksNotifier.Received().NotifyTrackHasBeenAddedToPlayList(aTrackId, aPlaylistId);
            await websocket.Received().PushMessageWithEventToAll(@event);
        }
    }
}