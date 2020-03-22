using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters;
using MyMusic.Infrastructure.Adapters.Http;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class PlayListServiceCreator {
        private readonly EventBusPortInMemoryAdapter eventBusPort;

        public PlayListServiceCreator(EventBusPortInMemoryAdapter eventBusPort) {
            this.eventBusPort = eventBusPort;
        }

        public CreatePlayListService CreateCreatePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var uniqueIdentifiersInMemoryAdapter = new UniqueIdentifiersInMemoryAdapter();
            return new CreatePlayListService(uniqueIdentifiersInMemoryAdapter, pLayListDatabaseAdapter, eventBusPort);
        }

        public RenamePlayListService CreateRenamePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new RenamePlayListService(pLayListDatabaseAdapter);
        }

        public ArchivePlayListService CreateArchivePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new ArchivePlayListService(pLayListDatabaseAdapter, eventBusPort);
        }

        public AddImageUrlToPlayListService CreateAddImageUrlPlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new AddImageUrlToPlayListService(pLayListDatabaseAdapter, musicCloudApiHttpAdapter);
        }
    }
}