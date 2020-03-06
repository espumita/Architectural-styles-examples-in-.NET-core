using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Successes;
using MyMusic.Application.SharedKernel.Model;

namespace MyMusic.Application.Services {
    public class AddImageUrlToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifierPort;
        
        public AddImageUrlToPlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifierPort) {
            this.playListPersistence = playListPersistence;
            this.playListNotifierPort = playListNotifierPort;
        }

        public Either<Error, ServiceResponse> Execute(string playlistId, string aNewImageUrL) {
            var playList = playListPersistence.GetPlayList(playlistId);
            playList.AddImageUrl(aNewImageUrL);
            playListPersistence.Persist(playList);
            playListNotifierPort.NotifyPlayListUrlHasChanged(playlistId, aNewImageUrL);
            return ServiceResponse.Success;
        }
    }
}