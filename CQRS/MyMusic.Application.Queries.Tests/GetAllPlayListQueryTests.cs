using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LanguageExt;
using MyMusic.Application.Queries.Errors;
using MyMusic.Application.Queries.Tests.builders;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Queries.Tests {

    public class GetAllPlayListQueryTests {
        private GetAllPlayListQuery getAllPlayListQuery;
        private PlayListQueryPort playListQueryPort;

        [SetUp]
        public void SetUp() {
            playListQueryPort = Substitute.For<PlayListQueryPort>();
            getAllPlayListQuery = new GetAllPlayListQuery(playListQueryPort);
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
            playListQueryPort.GetAllPlayList().Returns(new List<PlayList> {
                aPlayList, anotherPlayList
            });

            var result = getAllPlayListQuery.Execute();

            result.IsRight.Should().BeTrue();
            VerifyPLayListIsEquivalentTo(aPlayList, result);
        }

        private static void VerifyPLayListIsEquivalentTo(PlayList aPlayList, Either<QueryError, ListOfPlayLists> result) {
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