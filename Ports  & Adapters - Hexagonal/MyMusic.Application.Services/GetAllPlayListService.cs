using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Domain;

namespace MyMusic.Application.Services {

    public class GetAllPlayListService {
        private readonly PlayListPersistencePort playListPersistence;
        
        public GetAllPlayListService(PlayListPersistencePort playListPersistence) {
            this.playListPersistence = playListPersistence;
        }

        public Either<PlayListError, ListOfPlayLists> Execute() {
            var playLists = playListPersistence.GetAllPlayList();
            var activePlayLists = playLists
                    .Where(playList => playList.Status == PlayListStatus.Active)
                    .ToList();
            return new ListOfPlayLists(activePlayLists);
        }
    }
}