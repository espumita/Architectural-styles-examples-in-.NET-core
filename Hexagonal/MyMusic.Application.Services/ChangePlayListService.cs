using MyMusic.Model.PortsContracts.Http;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    public class ChangePlayListService {
        
        private readonly PlayListPersistencePort persistencePort;
        private readonly MusicHttpPort httpPort;

        public ChangePlayListService(PlayListPersistencePort persistencePort, MusicHttpPort httpPort) {
            this.persistencePort = persistencePort;
            this.httpPort = httpPort;
        }

        public void ChangeName(string playListId, string newPlayListName) {
            persistencePort.ChangePlayListName(playListId, newPlayListName);
            httpPort.NotifyPlayListNameHasChanged(playListId, newPlayListName);
        }
    }
}