using FluentAssertions;
using MyMusic.Api.Tests.CommandHandlers.builders;
using MyMusic.PlayList;
using MyMusic.PlayList.Domain;
using MyMusic.PlayList.Features;
using MyMusic.PlayList.Features.CreatePLayList;
using MyMusic.Shared.Ports;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.CommandHandlers {

    public class CreatePlayListCommandHandlerTests : CommandHandlerTest {
     
        private CreatePlayListCommandHandler createPlayListCommandHandler;
        private PlayListPersistencePort playListPersistence;
        private UniqueIdentifiersPort uniqueIdentifiers;
        private EventPublisherPort eventPublisher;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            uniqueIdentifiers = Substitute.For<UniqueIdentifiersPort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            createPlayListCommandHandler = new CreatePlayListCommandHandler(uniqueIdentifiers, playListPersistence, eventPublisher);
        }
        
        [Test]
        public void create_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            uniqueIdentifiers.GetNewUniqueIdentifier().Returns(aPlaylistId);
            var command = new CreatePLayList(aPlaylistName);

            var result = createPlayListCommandHandler.Handle(command);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, aPlaylistName, PlayListStatus.Active);
            VerifyEventHasBeenRaised(new PlayListHasBeenCreated(aPlaylistId, aPlaylistName), eventPublisher);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string aPlaylistName, PlayListStatus status) {
            playListPersistence.Received().Persist(Arg.Is<PlayList.Domain.PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Name.Equals(aPlaylistName)
                && playlist.Status.Equals(status)
            ));
        }
        
    }
}