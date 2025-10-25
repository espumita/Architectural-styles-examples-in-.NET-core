using MyMusic.Application.CommandHandlers;
using MyMusic.Application.Ports;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.CommandHandlerCreators {

    public class TracksCommandHandlerCreator {
        private readonly EventPublisherPort eventPublisher;

        public TracksCommandHandlerCreator(EventPublisherPort eventPublisher) {
            this.eventPublisher = eventPublisher;
        }

        public AddTrackToPlayListCommandHandler CreateAddTrackToPlayListCommandHandler() {
            var pLayListPostgreSqlAdapter = new PlayListPostgreSQLPersistenceAdapter();
            return new AddTrackToPlayListCommandHandler(pLayListPostgreSqlAdapter, eventPublisher);
        }
        
        public RemoveTrackFromPlayListCommandHandler CreateRemoveTrackFromPlayListCommandHandler() {
            var pLayListPostgreSqlAdapter = new PlayListPostgreSQLPersistenceAdapter();
            return new RemoveTrackFromPlayListCommandHandler(pLayListPostgreSqlAdapter, eventPublisher);
        }
    }
}