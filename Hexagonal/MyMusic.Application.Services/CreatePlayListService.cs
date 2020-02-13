using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;

namespace MyMusic.Application.Services {
    public class CreatePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;

        public CreatePlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public void Create(string playListName) {
            var playListId = playListPersistence.CreatePlayListFrom(playListName);
            playListNotifier.NotifyPlayListHasBeenCreated(playListId, playListName);
        }
        
    }
}