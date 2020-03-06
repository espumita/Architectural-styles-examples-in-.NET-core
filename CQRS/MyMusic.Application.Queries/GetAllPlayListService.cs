using LanguageExt;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using System.Linq;
using MyMusic.Application.SharedKernel.Model;

namespace MyMusic.Application.Queries {

    public class GetAllPlayListService {
        private readonly PlayListQueryPort playListPersistence;
        
        public GetAllPlayListService(PlayListQueryPort playListPersistence) {
            this.playListPersistence = playListPersistence;
        }

        public Either<Error, ListOfPlayLists> Execute() {
            var playLists = playListPersistence.GetAllPlayList();
            var activePlayLists = playLists
                    .Where(playList => playList.Status == PlayListStatus.Active)
                    .ToList();
            return new ListOfPlayLists(activePlayLists);
        }
    }
}