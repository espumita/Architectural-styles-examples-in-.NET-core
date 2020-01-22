using MyMusic.Model;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    public class GetPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        
        public GetPlayListService(PlayListPersistencePort playListPersistence) {
            this.playListPersistence = playListPersistence;
        }

        public PlayList Get(string playlistId) {
            return playListPersistence.GetPlayList(playlistId);
        }
    }
}