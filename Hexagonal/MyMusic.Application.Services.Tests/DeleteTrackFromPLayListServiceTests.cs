using FluentAssertions;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class DeleteTrackFromPLayListServiceTests {
        
        private DeleteTrackFromPLayListService deleteTrackFromPLayListService;
        private PlayListPersistencePort playListPersistence;
        private TracksNotifierPort tracksNotifier;
        
        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            deleteTrackFromPLayListService = new DeleteTrackFromPLayListService(playListPersistence, tracksNotifier);
        }
        
        [Test]
        public void remove_an_existing_track_from_a_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .AddTrack(new TrackBuilder()
                    .WithId(aTrackId)
                    .Build())
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);

            var result = deleteTrackFromPLayListService.Execute(aTrackId, aPlaylistId);

            result.IsRight.Should().BeTrue();
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist => 
                playlist.Id.Equals(aPlaylistId)
                && playlist.TrackList.Count.Equals(0)
            ));
            tracksNotifier.Received().NotifyTrackHasRemovedFromPlayList(aTrackId, aPlaylistId);
        }
    }
}