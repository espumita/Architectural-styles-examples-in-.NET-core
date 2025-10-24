using AwesomeAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.Services.Tests {

    public class CreatePlayListServiceTests {
     
        private CreatePlayListService createPlayListService;
        private PlayListPersistencePort playListPersistence;
        private PlayListNotifierPort playListNotifier;
        private UniqueIdentifiersPort uniqueIdentifiers;

        public CreatePlayListServiceTests() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            uniqueIdentifiers = Substitute.For<UniqueIdentifiersPort>();
            createPlayListService = new CreatePlayListService(uniqueIdentifiers, playListPersistence, playListNotifier);
        }
        
        [Fact]
        public void create_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            uniqueIdentifiers.GetNewUniqueIdentifier().Returns(aPlaylistId);
            
            var result = createPlayListService.Execute(aPlaylistName);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, aPlaylistName, PlayListStatus.Active);
            playListNotifier.Received().NotifyPlayListHasBeenCreated(aPlaylistId, aPlaylistName);
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