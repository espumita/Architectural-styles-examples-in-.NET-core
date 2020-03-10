using FluentAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class CreatePlayListServiceTests {
     
        private CreatePlayListService createPlayListService;
        private PlayListPersistencePort playListPersistence;
        private PlayListNotifierPort playListNotifierPort;
        private UniqueIdentifiersPort uniqueIdentifiersPort;
        private EventBus eventBus;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            playListNotifierPort = Substitute.For<PlayListNotifierPort>();
            uniqueIdentifiersPort = Substitute.For<UniqueIdentifiersPort>();
            eventBus = Substitute.For<EventBus>();
            createPlayListService = new CreatePlayListService(uniqueIdentifiersPort, playListPersistence, playListNotifierPort, eventBus);
        }
        
        [Test]
        public void create_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            uniqueIdentifiersPort.GetNewGuid().Returns(aPlaylistId);
            
            var result = createPlayListService.Execute(aPlaylistName);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, aPlaylistName, PlayListStatus.Active);
            playListNotifierPort.Received().NotifyPlayListHasBeenCreated(aPlaylistId, aPlaylistName);
            eventBus.Received().Raise(Arg.Is<PlayListHasBeenCreated>(@event => @event.Equals(new PlayListHasBeenCreated(aPlaylistId, aPlaylistName))));
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