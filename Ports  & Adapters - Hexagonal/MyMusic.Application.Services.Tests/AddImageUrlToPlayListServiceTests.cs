using AwesomeAssertions;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.Services.Tests {

    public class AddImageUrlToPlayListServiceTests {
        
        private AddImageUrlToPlayListService addImageUrlToPlayListService;
        private PlayListPersistencePort playListPersistence;
        private PlayListNotifierPort playListNotifier;

        public AddImageUrlToPlayListServiceTests() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            addImageUrlToPlayListService = new AddImageUrlToPlayListService(playListPersistence, playListNotifier);
        }

        [Fact]
        public void add_an_image_url_to_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var anImageUrl = APlaylist.AnotherImageUrl;
            
            var result = addImageUrlToPlayListService.Execute(aPlaylistId, anImageUrl);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, anImageUrl);
            playListNotifier.Received().NotifyPlayListUrlHasChanged(aPlaylistId, anImageUrl);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string anImageUrl) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.ImageUrl.Equals(anImageUrl)
            ));
        }
    }
}