using MyMusic.Model;
using MyMusic.Model.PortsContracts.Http;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    
    public class PlayListService {
        private readonly PlayListPersistencePort persistencePort;
        private readonly MusicHttpPort httpPort;

        public PlayListService(PlayListPersistencePort persistencePort) {
            this.persistencePort = persistencePort;
        }

        public PlayListService(PlayListPersistencePort persistencePort, MusicHttpPort httpPort) {
            this.persistencePort = persistencePort;
            this.httpPort = httpPort;
        }

        public PlayList Get(string playlistId) {
            return persistencePort.GetPlayList(playlistId);
        }

        public void Create(string playListName) {
            var playListId = persistencePort.CreatePlayListFrom(playListName);
            httpPort.NotifyPlayListHasBeenCreated(playListId, playListName);
        }

        public void ChangeName(string playListId, string newPlayListName) {
            persistencePort.ChangePlayListName(playListId, newPlayListName);
            httpPort.NotifyPlayListNameHasChanged(playListId, newPlayListName);
        }

        public void Delete(string playListId) {
            persistencePort.DeletePlayList(playListId);
            httpPort.NotifyPlayListHasBeenDeleted(playListId);
        }
    }
}