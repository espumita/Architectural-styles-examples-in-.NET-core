using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;

namespace MyMusic.Application.Services {
    public class AddImageUrlToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        
        public AddImageUrlToPlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playlistId, string aNewImageUrL) {
            var playList = playListPersistence.GetPlayList(playlistId);
            playList.AddImageUrl(aNewImageUrL);
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListUrlHasChanged(playlistId, aNewImageUrL);
            return ServiceResponse.Success;
        }
    }
}