using System.Linq;
using FluentAssertions;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class AddTrackToPLayListServiceTests {
        
        private AddTrackToPlayListService addTrackToPlayListService;
        private PlayListPersistencePort playListPersistence;
        private TracksNotifierPort tracksNotifier;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            addTrackToPlayListService = new AddTrackToPlayListService(playListPersistence, tracksNotifier);
        }
        
        [Test]
        public void add_a_track_to_a_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);

            var result = addTrackToPlayListService.Execute(aTrackId, aPlaylistId);

            result.IsRight.Should().BeTrue();
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist => 
                playlist.Id.Equals(aPlaylistId)
                && playlist.TrackList.Single().Id.Equals(aTrackId)
            ));
        }

        [Test]
        public void do_not_add_a_track_twice() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .AddTrack(new TrackBuilder()
                    .WithId(aTrackId)
                    .Build())
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);

            var result = addTrackToPlayListService.Execute(aTrackId, aPlaylistId);

            result.IsLeft.Should().BeTrue();

        }
        
    }
}