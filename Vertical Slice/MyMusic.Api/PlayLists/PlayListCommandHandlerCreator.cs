using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePLayList;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Persistence;

namespace MyMusic.PlayLists {

    public class PlayListCommandHandlerCreator {
        private readonly EventPublisher eventPublisher;

        public PlayListCommandHandlerCreator(EventPublisher eventPublisher) {
            this.eventPublisher = eventPublisher;
        }

        public CreatePlayListCommandHandler CreateCreatePlayListCommandHandler() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistence();
            var uniqueIdentifiersInMemoryAdapter = new UniqueIdentifiersInMemory();
            return new CreatePlayListCommandHandler(uniqueIdentifiersInMemoryAdapter, pLayListDatabaseAdapter, eventPublisher);
        }

        public RenamePlayListCommandHandler CreateRenamePlayListCommandHandler() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistence();
            return new RenamePlayListCommandHandler(pLayListDatabaseAdapter, eventPublisher);
        }

        public ArchivePlayListCommandHandler CreateArchivePlayListCommandHandler() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistence();
            return new ArchivePlayListCommandHandler(pLayListDatabaseAdapter, eventPublisher);
        }

        public AddImageUrlToPlayListCommandHandler CreateAddImageUrlPlayListCommandHandler() {
            var pLayListDatabaseAdapter = new PLayListPostgreSQLPersistence();
            return new AddImageUrlToPlayListCommandHandler(pLayListDatabaseAdapter, eventPublisher);
        }
    }
}