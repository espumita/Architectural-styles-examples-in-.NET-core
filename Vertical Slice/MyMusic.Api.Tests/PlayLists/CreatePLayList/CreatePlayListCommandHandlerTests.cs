using FluentAssertions;
using MyMusic.Api.Tests.Shared;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.CreatePLayList;
using MyMusic.Shared.Domain;
using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Persistence;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.PlayLists.CreatePLayList {

    public class CreatePlayListCommandHandlerTests : CommandHandlerTest {
     
        private CreatePlayListCommandHandler createPlayListCommandHandler;
        private PlayListPersistence playListPersistence;
        private UniqueIdentifiers uniqueIdentifiers;
        private EventPublisher eventPublisher;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistence>();
            uniqueIdentifiers = Substitute.For<UniqueIdentifiers>();
            eventPublisher = Substitute.For<EventPublisher>();
            createPlayListCommandHandler = new CreatePlayListCommandHandler(uniqueIdentifiers, playListPersistence, eventPublisher);
        }
        
        [Test]
        public void create_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            uniqueIdentifiers.GetNewUniqueIdentifier().Returns(aPlaylistId);
            var command = new MyMusic.PlayLists.Features.CreatePLayList.CreatePLayList(aPlaylistName);

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