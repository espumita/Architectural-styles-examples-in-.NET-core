using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class GetAllPlayListServiceTests {
        private GetAllPlayListService getAllPlayListService;
        private PlayListPersistencePort playListPersistencePort;

        [SetUp]
        public void SetUp() {
            playListPersistencePort = Substitute.For<PlayListPersistencePort>();
            getAllPlayListService = new GetAllPlayListService(playListPersistencePort);
        }

        [Test]
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
                    .WithDuration(ATrack.DurationInMs)
                    .Build())
                .Build();
            var anotherPlayListId = APlaylist.AnotherId;
            var anotherPlayList = new PlayListBuilder()
                .WithId(anotherPlayListId)
                .WithStatus(PlayListStatus.Archived)
                .Build();
            playListPersistencePort.GetAllPlayList().Returns(new List<PlayList> {
                aPlayList, anotherPlayList
            });

            var result = getAllPlayListService.Execute();

            result.IsRight.Should().BeTrue();
            VerifyPLayListIsEquivalentTo(aPlayList, result);
        }

        private static void VerifyPLayListIsEquivalentTo(PlayList aPlayList, Either<ServiceError, ListOfPlayLists> result) {
            result.Match(
                Right: listOfPlayLists => Validate(listOfPlayLists, aPlayList),
                Left: error => null
            );
        }

        private static PlayList Validate(ListOfPlayLists playListsList, PlayList expectedPlayList) {
            var playList = playListsList.Elements.Single();
            playList.Should().BeEquivalentTo(expectedPlayList);
            return expectedPlayList;
        }
    }
}