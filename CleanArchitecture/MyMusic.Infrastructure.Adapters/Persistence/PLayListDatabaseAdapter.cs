using MyMusic.Model;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Infrastructure.Persistence {
    public class PLayListDatabaseAdapter : PlayListPersistencePort {
        public PlayList GetPlayList(string playlistId) {
            //This should be read from persistence
            return new PlayList(playlistId);
        }

        public void CreatePlayListFrom(string playListName) {
            //This should save in persistence
        }

        public void ChangePlayListName(string playListId, string newPlayListName) {
            //This should update in persistence
        }

        public void DeletePlayList(object playListId) {
            //This should delete in persistence
        }
    }
}