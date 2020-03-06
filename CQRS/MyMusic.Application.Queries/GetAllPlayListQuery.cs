using LanguageExt;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using System.Linq;
using MyMusic.Application.SharedKernel.Model;

namespace MyMusic.Application.Queries {

    public class GetAllPlayListQuery {
        private readonly PlayListQueryPort playListQueryPort;
        
        public GetAllPlayListQuery(PlayListQueryPort playListQueryPort) {
            this.playListQueryPort = playListQueryPort;
        }

        public Either<Error, ListOfPlayLists> Execute() {
            var playLists = playListQueryPort.GetAllPlayList();
            var activePlayLists = playLists
                    .Where(playList => playList.Status == PlayListStatus.Active)
                    .ToList();
            return new ListOfPlayLists(activePlayLists);
        }
    }
}