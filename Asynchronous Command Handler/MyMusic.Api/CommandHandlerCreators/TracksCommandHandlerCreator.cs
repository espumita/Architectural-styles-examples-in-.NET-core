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
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new AddTrackToPlayListCommandHandler(pLayListPostgreSqlAdapter, eventPublisher);
        }
        
        public RemoveTrackFromPLayListCommandHandler CreateRemoveTrackFromPLayListCommandHandler() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new RemoveTrackFromPLayListCommandHandler(pLayListPostgreSqlAdapter, eventPublisher);
        }
    }
}