using MyMusic.Model.PortsContracts.Http;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    
    public class DeletePlayListService {
        
        private readonly PlayListPersistencePort persistencePort;
        private readonly MusicHttpPort httpPort;

        public DeletePlayListService(PlayListPersistencePort persistencePort, MusicHttpPort httpPort) {
            this.persistencePort = persistencePort;
            this.httpPort = httpPort;
        }

        public void Delete(string playListId) {
            persistencePort.DeletePlayList(playListId);
            httpPort.NotifyPlayListHasBeenDeleted(playListId);
        }
    }
}