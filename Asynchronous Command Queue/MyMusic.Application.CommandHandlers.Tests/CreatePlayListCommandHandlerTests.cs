using AwesomeAssertions;
using MyMusic.Application.CommandHandlers.Tests.builders;
using MyMusic.Application.Commands;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.CommandHandlers.Tests {

    public class CreatePlayListCommandHandlerTests : CommandHandlerTest {
     
        private CreatePlayListCommandHandler createPlayListCommandHandler;
        private PlayListPersistencePort playListPersistence;
        private UniqueIdentifiersPort uniqueIdentifiers;
        private EventPublisherPort eventPublisher;

        public CreatePlayListCommandHandlerTests() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            uniqueIdentifiers = Substitute.For<UniqueIdentifiersPort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            createPlayListCommandHandler = new CreatePlayListCommandHandler(uniqueIdentifiers, playListPersistence, eventPublisher);
        }
        
        [Fact]
        public void create_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            uniqueIdentifiers.GetNewUniqueIdentifier().Returns(aPlaylistId);
            var command = new CreatePlayList(aPlaylistName);

            var result = createPlayListCommandHandler.Handle(command);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, aPlaylistName, PlayListStatus.Active);
            VerifyEventHasBeenRaised(new PlayListHasBeenCreated(aPlaylistId, aPlaylistName), eventPublisher);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string aPlaylistName, PlayListStatus status) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Name.Equals(aPlaylistName)
                && playlist.Status.Equals(status)
            ));
        }
        
    }
}