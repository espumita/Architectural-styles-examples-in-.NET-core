using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Persistence;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.Tracks {

    public class TracksCommandHandlerCreator {
        private readonly EventPublisher eventPublisher;

        public TracksCommandHandlerCreator(EventPublisher eventPublisher) {
            this.eventPublisher = eventPublisher;
        }

        public AddTrackToPlayListCommandHandler CreateAddTrackToPlayListCommandHandler() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistence();
            return new AddTrackToPlayListCommandHandler(pLayListPostgreSqlAdapter, eventPublisher);
        }
        
        public RemoveTrackFromPLayListCommandHandler CreateRemoveTrackFromPLayListCommandHandler() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistence();
            return new RemoveTrackFromPLayListCommandHandler(pLayListPostgreSqlAdapter, eventPublisher);
        }
    }
}