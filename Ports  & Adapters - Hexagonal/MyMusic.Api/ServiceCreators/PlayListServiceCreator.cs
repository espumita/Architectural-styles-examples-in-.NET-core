using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters;
using MyMusic.Infrastructure.Adapters.Http;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class PlayListServiceCreator {
        
        public GetAllPlayListService CreateGetAllPlayListService() {
            var playListDatabaseAdapter = new PlayListPostgreSQLAdapter();
            return new GetAllPlayListService(playListDatabaseAdapter);
        }
        
        public GetPlayListService CreateGetPlayListService() {
            var playListDatabaseAdapter = new PlayListPostgreSQLAdapter();
            return new GetPlayListService(playListDatabaseAdapter);
        }

        public CreatePlayListService CreateCreatePlayListService() {
            var pLayListDatabaseAdapter = new PlayListPostgreSQLAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            var uniqueIdentifiersInMemoryAdapter = new UniqueIdentifiersInMemoryAdapter();
            return new CreatePlayListService(uniqueIdentifiersInMemoryAdapter, pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }

        public RenamePlayListService CreateRenamePlayListService() {
            var pLayListDatabaseAdapter = new PlayListPostgreSQLAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new RenamePlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }

        public ArchivePlayListService CreateArchivePlayListService() {
            var pLayListDatabaseAdapter = new PlayListPostgreSQLAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new ArchivePlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }

        public AddImageUrlToPlayListService CreateAddImageUrlPlayListService() {
            var pLayListDatabaseAdapter = new PlayListPostgreSQLAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new AddImageUrlToPlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }
    }
}