using FluentAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class CreatePlayListServiceTests {
     
        private CreatePlayListService createPlayListService;
        private PlayListPersistencePort playListPersistence;
        private PlayListNotifierPort playListNotifierPort;
        private UniqueIdentifiersPort uniqueIdentifiersPort;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            playListNotifierPort = Substitute.For<PlayListNotifierPort>();
            uniqueIdentifiersPort = Substitute.For<UniqueIdentifiersPort>();
            createPlayListService = new CreatePlayListService(uniqueIdentifiersPort, playListPersistence, playListNotifierPort);
        }
        
        [Test]
        public void create_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            uniqueIdentifiersPort.GetNewGuid().Returns(aPlaylistId);
            
            var result = createPlayListService.Execute(aPlaylistName);
            
            result.IsRight.Should().BeTrue();
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist => 
                playlist.Id.Equals(aPlaylistId)
                && playlist.Name.Equals(aPlaylistName)
                && playlist.Status.Equals(PlayListStatus.Active)
            ));
            playListNotifierPort.Received().NotifyPlayListHasBeenCreated(aPlaylistId, aPlaylistName);
        }

    }
}