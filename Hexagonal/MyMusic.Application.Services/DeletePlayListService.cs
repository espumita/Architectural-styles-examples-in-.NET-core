using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;

namespace MyMusic.Application.Services {
    
    public class DeletePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;

        public DeletePlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public void Delete(string playListId) {
            playListPersistence.DeletePlayList(playListId);
            playListNotifier.NotifyPlayListHasBeenDeleted(playListId);
        }
    }
}