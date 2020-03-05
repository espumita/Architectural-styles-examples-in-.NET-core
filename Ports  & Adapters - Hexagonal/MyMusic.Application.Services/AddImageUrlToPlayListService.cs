using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;

namespace MyMusic.Application.Services {

    public class AddImageUrlToPlayListService {
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifierPort;
        private const string OperationSuccess = "OperationSuccess";
        public AddImageUrlToPlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifierPort) {
            this.playListPersistence = playListPersistence;
            this.playListNotifierPort = playListNotifierPort;
        }

        public Either<PlayListError, string> Execute(string playlistId, string aNewImageUrL) {
            var playList = playListPersistence.GetPlayList(playlistId);
            playList.AddImageUrl(aNewImageUrL);
            playListPersistence.Persist(playList);
            playListNotifierPort.NotifyPlayListUrlHasChanged(playlistId, aNewImageUrL);
            return OperationSuccess;
        }
    }
}