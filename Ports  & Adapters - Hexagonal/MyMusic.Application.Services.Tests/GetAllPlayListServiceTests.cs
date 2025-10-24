using System.Collections.Generic;
using System.Linq;
using AwesomeAssertions;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.Services.Tests {

    public class GetAllPlayListServiceTests {
        private GetAllPlayListService getAllPlayListService;
        private PlayListPersistencePort playListPersistence;

        public GetAllPlayListServiceTests() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            getAllPlayListService = new GetAllPlayListService(playListPersistence);
        }

        [Fact]
        public void get_active_play_lists() {
            var aPlayListId = APlaylist.Id;
            var aPlayListName = APlaylist.Name;
            var aPlayListImageUrl = APlaylist.ImageUrl;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlayListId)
                .WithName(aPlayListName)
                .WithImageUrl(aPlayListImageUrl)
                .WithStatus(PlayListStatus.Active)
                .AddTrack(new TrackBuilder()
                    .WithId(ATrack.Id)
                    .WithName(ATrack.Name)
                    .WithArtist(ATrack.Artist)
                    .WithDuration(ATrack.DurationInMs)
                    .Build())
                .Build();
            var anotherPlayListId = APlaylist.AnotherId;
            var anotherPlayList = new PlayListBuilder()
                .WithId(anotherPlayListId)
                .WithStatus(PlayListStatus.Archived)
                .Build();
            playListPersistence.GetAllPlayList().Returns(new List<PlayList> {
                aPlayList, anotherPlayList
            });

            var result = getAllPlayListService.Execute();

            result.IsRight.Should().BeTrue();
            result.IfRight(listOfPlayLists => VerifyAreEquivalent(listOfPlayLists, aPlayList));
        }

        private static void VerifyAreEquivalent(ListOfPlayLists playListsList, PlayList expectedPlayList) {
            var playList = playListsList.Elements.Single();
            playList.Should().BeEquivalentTo(expectedPlayList);
        }
    }
}