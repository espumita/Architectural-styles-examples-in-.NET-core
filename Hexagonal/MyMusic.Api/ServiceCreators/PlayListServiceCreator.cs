using MyMusic.Application.Services;
using MyMusic.Infrastructure.Http;
using MyMusic.Infrastructure.Persistence;

namespace MyMusic.ServiceCreators {

    public class PlayListServiceCreator {
        public GetPlayListService CreateGetPlayListService() {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            return new GetPlayListService(playListDatabaseAdapter);
        }

        public CreatePlayListService CreateCreatePlayListService() {
            var pLayListDatabaseAdapter = new PLayListDatabaseAdapter();
            var musicCloudApiHttpAdapter = new MusicCloudApiHttpAdapter();
            return new CreatePlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }

        public ChangePlayListService CreateChangePlayListService() {
            var pLayListDatabaseAdapter = new PLayListDatabaseAdapter();
            var musicCloudApiHttpAdapter = new MusicCloudApiHttpAdapter();
            return new ChangePlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }

        public DeletePlayListService CreateDeletePlayListService() {
            var pLayListDatabaseAdapter = new PLayListDatabaseAdapter();
            var musicCloudApiHttpAdapter = new MusicCloudApiHttpAdapter();
            return new DeletePlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }
    }
}