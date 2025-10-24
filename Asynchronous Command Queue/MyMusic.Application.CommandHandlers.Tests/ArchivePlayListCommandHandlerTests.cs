using FluentAssertions;
using MyMusic.Application.CommandHandlers.Tests.builders;
using MyMusic.Application.Commands;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.CommandHandlers.Tests {

    public class ArchivePlayListCommandHandlerTests : CommandHandlerTest {
        
        private ArchivePlayListCommandHandler archivePlayListCommandHandler;
        private PlayListPersistencePort playListPersistence;
        private EventPublisherPort eventPublisher;

        public ArchivePlayListCommandHandlerTests() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            archivePlayListCommandHandler = new ArchivePlayListCommandHandler(playListPersistence, eventPublisher);
        }

        [Fact]
        public void archive_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .WithStatus(PlayListStatus.Active)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var command = new ArchivePlayList(aPlaylistId);

            var result = archivePlayListCommandHandler.Handle(command);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, PlayListStatus.Archived);
            VerifyEventHasBeenRaised(new PlayListHasBeenArchived(aPlaylistId), eventPublisher);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, PlayListStatus status) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Status.Equals(status)
                           ));
        }
    }
}