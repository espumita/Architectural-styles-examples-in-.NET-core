using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePLayList;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Ports;

namespace MyMusic.PlayLists {

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