using MyMusic.Application.CommandHandlers;
using MyMusic.Application.Ports;
using MyMusic.Infrastructure.Adapters;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.CommandHandlerCreators {

    public class PlayListCommandHandlerCreator {
        private readonly EventPublisherPort eventPublisher;

        public PlayListCommandHandlerCreator(EventPublisherPort eventPublisher) {
            this.eventPublisher = eventPublisher;
        }

        public CreatePlayListCommandHandler CreateCreatePlayListCommandHandler() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var uniqueIdentifiersInMemoryAdapter = new UniqueIdentifiersInMemoryAdapter();
            return new CreatePlayListCommandHandler(uniqueIdentifiersInMemoryAdapter, pLayListDatabaseAdapter, eventPublisher);
        }

        public RenamePlayListCommandHandler CreateRenamePlayListCommandHandler() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new RenamePlayListCommandHandler(pLayListDatabaseAdapter, eventPublisher);
        }

        public ArchivePlayListCommandHandler CreateArchivePlayListCommandHandler() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new ArchivePlayListCommandHandler(pLayListDatabaseAdapter, eventPublisher);
        }

        public AddImageUrlToPlayListCommandHandler CreateAddImageUrlPlayListCommandHandler() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new AddImageUrlToPlayListCommandHandler(pLayListDatabaseAdapter, eventPublisher);
        }
    }
}