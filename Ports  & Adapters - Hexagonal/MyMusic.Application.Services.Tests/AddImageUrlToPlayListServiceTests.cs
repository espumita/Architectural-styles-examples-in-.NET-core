using FluentAssertions;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class AddImageUrlToPlayListServiceTests {
        
        private AddImageUrlToPlayListService addImageUrlToPlayListService;
        private PlayListPersistencePort playListPersistence;
        private PlayListNotifierPort playListNotifierPort;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            playListNotifierPort = Substitute.For<PlayListNotifierPort>();
            addImageUrlToPlayListService = new AddImageUrlToPlayListService(playListPersistence, playListNotifierPort);
        }

        [Test]
        public void add_an_image_url_to_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var anImageUrl = APlaylist.AnotherImageUrl;
            
            var result = addImageUrlToPlayListService.Execute(aPlaylistId, anImageUrl);
            
            result.IsRight.Should().BeTrue();
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist => 
                playlist.Id.Equals(aPlaylistId)
                && playlist.ImageUrl.Equals(anImageUrl)
            ));
            playListNotifierPort.Received().NotifyPlayListUrlHasChanged(aPlaylistId, anImageUrl);
        }
    }
}