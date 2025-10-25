using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePlayList;
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
            var playListDatabaseAdapter = new PlayListPostgreSQLPersistence();
            var uniqueIdentifiersInMemoryAdapter = new UniqueIdentifiersInMemory();
            return new CreatePlayListCommandHandler(uniqueIdentifiersInMemoryAdapter, playListDatabaseAdapter, eventPublisher);
        }

        public RenamePlayListCommandHandler CreateRenamePlayListCommandHandler() {
            var playListDatabaseAdapter = new PlayListPostgreSQLPersistence();
            return new RenamePlayListCommandHandler(playListDatabaseAdapter, eventPublisher);
        }

        public ArchivePlayListCommandHandler CreateArchivePlayListCommandHandler() {
            var playListDatabaseAdapter = new PlayListPostgreSQLPersistence();
            return new ArchivePlayListCommandHandler(playListDatabaseAdapter, eventPublisher);
        }

        public AddImageUrlToPlayListCommandHandler CreateAddImageUrlPlayListCommandHandler() {
            var playListDatabaseAdapter = new PlayListPostgreSQLPersistence();
            return new AddImageUrlToPlayListCommandHandler(playListDatabaseAdapter, eventPublisher);
        }
    }
}