using FluentAssertions;
using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
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
        
        [Test]
        public void do_not_remove_a_track_when_it_is_not_already_in_the_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);

            var result = deleteTrackFromPLayListService.Execute(aTrackId, aPlaylistId);

            result.IsLeft.Should().BeTrue();
            VerifyErrorIs(PlayListError.TrackIsNotInThePlayList, result);
            playListPersistence.DidNotReceive().Persist(Arg.Any<PlayList>());
            tracksNotifier.DidNotReceive().NotifyTrackHasRemovedFromPlayList(Arg.Any<string>(), Arg.Any<string>());
        }
        
        private static void VerifyErrorIs(PlayListError playListError, Either<PlayListError, string> result) {
            result.Match(
                Left: x => x.Should().Be(playListError),
                Right: null);
        }
    }
}