using System.Collections.Generic;
using FluentAssertions;
using MyMusic.Api.Tests.Shared;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Domain;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.PlayLists.Features;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.Tracks.RemoveTrackFromPlayList {

    public class RemoveTrackFromPLayListCommandHandlerTests : CommandHandlerTest {
        
        private RemoveTrackFromPLayListCommandHandler removeTrackFromPLayListCommandHandler;
        private PlayListPersistence playListPersistence;
        private EventPublisher eventPublisher;
        
        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistence>();
            eventPublisher = Substitute.For<EventPublisher>();
            removeTrackFromPLayListCommandHandler = new RemoveTrackFromPLayListCommandHandler(playListPersistence, eventPublisher);
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
            var command = new MyMusic.Tracks.Features.RemoveTrackFromPlayList.RemoveTrackFromPlayList(aTrackId, aPlaylistId);

            var result = removeTrackFromPLayListCommandHandler.Handle(command);

            result.IsRight.Should().BeTrue();
            VerifyAnEmptyPlayListHasBeenPersistedWith(aPlaylistId);
            VerifyEventHasBeenRaised(new TrackHasBeenRemovedFromPlayList(aTrackId, aPlaylistId), eventPublisher);
        }

        [Test]
        public void do_not_remove_a_track_when_it_is_not_already_in_the_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var command = new MyMusic.Tracks.Features.RemoveTrackFromPlayList.RemoveTrackFromPlayList(aTrackId, aPlaylistId);
            
            var result = removeTrackFromPLayListCommandHandler.Handle(command);

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