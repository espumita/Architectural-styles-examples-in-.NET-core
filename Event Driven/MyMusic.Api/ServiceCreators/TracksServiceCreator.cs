using MyMusic.Application.Ports;
using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class TracksServiceCreator {
        private readonly EventBusPort eventBus;

        public TracksServiceCreator(EventBusPort eventBus) {
            this.eventBus = eventBus;
        }

        public AddTrackToPlayListService CreateAddTrackToPlayListService() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new AddTrackToPlayListService(pLayListPostgreSqlAdapter, eventBus);
        }
        
        public RemoveTrackFromPLayListService CreateRemoveTrackFromPLayListService() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new RemoveTrackFromPLayListService(pLayListPostgreSqlAdapter, eventBus);
        }
    }
}