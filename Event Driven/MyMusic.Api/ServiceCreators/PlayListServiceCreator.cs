using MyMusic.Application.Ports;
using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class PlayListServiceCreator {
        private readonly EventPublisherPort eventPublisher;

        public PlayListServiceCreator(EventPublisherPort eventPublisher) {
            this.eventPublisher = eventPublisher;
        }

        public CreatePlayListService CreateCreatePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var uniqueIdentifiersInMemoryAdapter = new UniqueIdentifiersInMemoryAdapter();
            return new CreatePlayListService(uniqueIdentifiersInMemoryAdapter, pLayListDatabaseAdapter, eventPublisher);
        }

        public RenamePlayListService CreateRenamePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new RenamePlayListService(pLayListDatabaseAdapter, eventPublisher);
        }

        public ArchivePlayListService CreateArchivePlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new ArchivePlayListService(pLayListDatabaseAdapter, eventPublisher);
        }

        public AddImageUrlToPlayListService CreateAddImageUrlPlayListService() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new AddImageUrlToPlayListService(pLayListDatabaseAdapter, eventPublisher);
        }
    }
}