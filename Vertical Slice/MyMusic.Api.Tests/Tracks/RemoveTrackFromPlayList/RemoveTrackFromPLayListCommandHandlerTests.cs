using System.Collections.Generic;
using AwesomeAssertions;
using MyMusic.Api.Tests.Shared;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.Shared;
using MyMusic.Shared.Domain;
using MyMusic.Shared.Domain.Error;
using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Persistence;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;
using NSubstitute;
using Xunit;

namespace MyMusic.Api.Tests.Tracks.RemoveTrackFromPlayList {

    public class RemoveTrackFromPlayListCommandHandlerTests : CommandHandlerTest {
        
        private RemoveTrackFromPlayListCommandHandler removeTrackFromPlayListCommandHandler;
        private PlayListPersistence playListPersistence;
        private EventPublisher eventPublisher;
        
        public RemoveTrackFromPlayListCommandHandlerTests() {
            playListPersistence = Substitute.For<PlayListPersistence>();
            eventPublisher = Substitute.For<EventPublisher>();
            removeTrackFromPlayListCommandHandler = new RemoveTrackFromPlayListCommandHandler(playListPersistence, eventPublisher);
        }
        
        [Fact]
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
            var command = new MyMusic.Tracks.Features.RemoveTrackFromPlayList.RemoveTrackFromPlayList(aTrackId, aPlaylistId);

            var result = removeTrackFromPlayListCommandHandler.Handle(command);

            result.IsRight.Should().BeTrue();
            VerifyAnEmptyPlayListHasBeenPersistedWith(aPlaylistId);
            VerifyEventHasBeenRaised(new TrackHasBeenRemovedFromPlayList(aTrackId, aPlaylistId), eventPublisher);
        }

        [Fact]
        public void do_not_remove_a_track_when_it_is_not_already_in_the_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var command = new MyMusic.Tracks.Features.RemoveTrackFromPlayList.RemoveTrackFromPlayList(aTrackId, aPlaylistId);
            
            var result = removeTrackFromPlayListCommandHandler.Handle(command);

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().Be(DomainError.TrackIsNotInThePlayList));
            playListPersistence.DidNotReceive().Persist(Arg.Any<PlayList>());
            eventPublisher.DidNotReceive().Publish(Arg.Any<List<Event>>());
        }

        private void VerifyAnEmptyPlayListHasBeenPersistedWith(string aPlaylistId) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.TrackList.Count.Equals(0)
            ));
        }
        
    }
}