using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;

namespace MyMusic.Application.Services {
    public class ChangePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;

        public ChangePlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public void ChangeName(string playListId, string newPlayListName) {
            playListPersistence.ChangePlayListName(playListId, newPlayListName);
            playListNotifier.NotifyPlayListNameHasChanged(playListId, newPlayListName);
        }
    }
}