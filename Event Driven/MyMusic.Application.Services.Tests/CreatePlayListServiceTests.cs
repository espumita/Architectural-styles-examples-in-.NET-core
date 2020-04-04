using FluentAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class CreatePlayListServiceTests : ServiceTest {
     
        private CreatePlayListService createPlayListService;
        private PlayListPersistencePort playListPersistence;
        private UniqueIdentifiersPort uniqueIdentifiers;
        private EventBusPort eventBus;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            uniqueIdentifiers = Substitute.For<UniqueIdentifiersPort>();
            eventBus = Substitute.For<EventBusPort>();
            createPlayListService = new CreatePlayListService(uniqueIdentifiers, playListPersistence, eventBus);
        }
        
        [Test]
        public void create_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            uniqueIdentifiers.GetNewUniqueIdentifier().Returns(aPlaylistId);
            
            var result = createPlayListService.Execute(aPlaylistName);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, aPlaylistName, PlayListStatus.Active);
            VerifyEventHasBeenRaised(new PlayListHasBeenCreated(aPlaylistId, aPlaylistName), eventBus);
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