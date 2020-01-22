using MyMusic.Model;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    public class GetPlayListService {
        
        private readonly PlayListPersistencePort persistencePort;
        
        public GetPlayListService(PlayListPersistencePort persistencePort) {
            this.persistencePort = persistencePort;
        }

        public PlayList Get(string playlistId) {
            return persistencePort.GetPlayList(playlistId);
        }
    }
}