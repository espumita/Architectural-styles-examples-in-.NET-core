using MyMusic.PlayList.Features;
using MyMusic.PlayList.Features.ArchivePlayList;
using MyMusic.PlayList.Features.ChangePlayListImageUrl;
using MyMusic.PlayList.Features.CreatePLayList;
using MyMusic.PlayList.Features.RenamePlaylist;
using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Ports;

namespace MyMusic.PlayList {

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