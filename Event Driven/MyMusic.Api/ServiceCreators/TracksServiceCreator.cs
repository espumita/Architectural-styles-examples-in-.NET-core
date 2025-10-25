using MyMusic.Application.Ports;
using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class TracksServiceCreator {
        private readonly EventPublisherPort eventPublisher;

        public TracksServiceCreator(EventPublisherPort eventPublisher) {
            this.eventPublisher = eventPublisher;
        }

        public AddTrackToPlayListService CreateAddTrackToPlayListService() {
            var pLayListPostgreSqlAdapter = new PlayListPostgreSQLPersistenceAdapter();
            return new AddTrackToPlayListService(pLayListPostgreSqlAdapter, eventPublisher);
        }
        
        public RemoveTrackFromPlayListService CreateRemoveTrackFromPlayListService() {
            var pLayListPostgreSqlAdapter = new PlayListPostgreSQLPersistenceAdapter();
            return new RemoveTrackFromPlayListService(pLayListPostgreSqlAdapter, eventPublisher);
        }
    }
}