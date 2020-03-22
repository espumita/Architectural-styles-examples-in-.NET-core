using MyMusic.Application.Ports;
using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class PlayListServiceCreator {
        private readonly EventBusPort eventBus;

        public PlayListServiceCreator(EventBusPort eventBus) {
            this.eventBus = eventBus;
        }

        public CreatePlayListService CreateCreatePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var uniqueIdentifiersInMemoryAdapter = new UniqueIdentifiersInMemoryAdapter();
            return new CreatePlayListService(uniqueIdentifiersInMemoryAdapter, pLayListDatabaseAdapter, eventBus);
        }

        public RenamePlayListService CreateRenamePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new RenamePlayListService(pLayListDatabaseAdapter, eventBus);
        }

        public ArchivePlayListService CreateArchivePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new ArchivePlayListService(pLayListDatabaseAdapter, eventBus);
        }

        public AddImageUrlToPlayListService CreateAddImageUrlPlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new AddImageUrlToPlayListService(pLayListDatabaseAdapter, eventBus);
        }
    }
}