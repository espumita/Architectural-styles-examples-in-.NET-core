using MyMusic.Model;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    
    public class PlayListService {
        private readonly PlayListPersistencePort persistencePort;

        public PlayListService(PlayListPersistencePort persistencePort) {
            this.persistencePort = persistencePort;
        }

        public PlayList Get(string playlistId) {
            return persistencePort.GetPlayList(playlistId);
        }

        public void Create(string playListName) {
            persistencePort.CreatePlayListFrom(playListName);
        }

        public void ChangeName(string playListId, string newPlayListName) {
            persistencePort.ChangePlayListName(playListId, newPlayListName);
        }

        public void Delete(string playListId) {
            persistencePort.DeletePlayList(playListId);
        }
    }
}