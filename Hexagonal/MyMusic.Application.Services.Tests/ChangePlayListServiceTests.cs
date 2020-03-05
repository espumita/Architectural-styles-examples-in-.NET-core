using System.Linq;
using FluentAssertions;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class ChangePlayListServiceTests {
        
        private ChangePlayListService changePlayListService;
        private PlayListPersistencePort playListPersistence;
        private PlayListNotifierPort playListNotifierPort;


        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            playListNotifierPort = Substitute.For<PlayListNotifierPort>();
            changePlayListService = new ChangePlayListService(playListPersistence, playListNotifierPort);
        }


        [Test]
        public void change_play_list_name() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .WithName(aPlaylistName)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var anotherPlaylistName = APlaylist.AnotherName;
            
            var result = changePlayListService.Execute(aPlaylistId, anotherPlaylistName);
            
            result.IsRight.Should().BeTrue();
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist => 
                playlist.Id.Equals(aPlaylistId)
                && playlist.Name.Equals(anotherPlaylistName)
            ));
            playListNotifierPort.Received().NotifyPlayListNameHasChanged(aPlaylistId, anotherPlaylistName);
        }
    }
}