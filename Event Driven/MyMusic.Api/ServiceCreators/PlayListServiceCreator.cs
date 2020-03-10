using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters;
using MyMusic.Infrastructure.Adapters.Http;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class PlayListServiceCreator {
        private readonly EventBusInMemoryAdapter eventBus;

        public PlayListServiceCreator(EventBusInMemoryAdapter eventBus) {
            this.eventBus = eventBus;
        }

        public CreatePlayListService CreateCreatePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            var uniqueIdentifiersInMemoryAdapter = new UniqueIdentifiersInMemoryAdapter();
            return new CreatePlayListService(uniqueIdentifiersInMemoryAdapter, pLayListDatabaseAdapter, musicCloudApiHttpAdapter, eventBus);
        }

        public RenamePlayListService CreateRenamePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new RenamePlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }

        public ArchivePlayListService CreateArchivePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new ArchivePlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }

        public AddImageUrlToPlayListService CreateAddImageUrlPlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new AddImageUrlToPlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }
    }
}