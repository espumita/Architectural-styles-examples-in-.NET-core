using MyMusic.Model.PortsContracts.Http;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    public class CreatePlayListService {
        
        private readonly PlayListPersistencePort persistencePort;
        private readonly MusicHttpPort httpPort;

        public CreatePlayListService(PlayListPersistencePort persistencePort, MusicHttpPort httpPort) {
            this.persistencePort = persistencePort;
            this.httpPort = httpPort;
        }

        public void Create(string playListName) {
            var playListId = persistencePort.CreatePlayListFrom(playListName);
            httpPort.NotifyPlayListHasBeenCreated(playListId, playListName);
        }
        
    }
}