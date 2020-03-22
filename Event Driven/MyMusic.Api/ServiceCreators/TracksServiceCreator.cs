using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class TracksServiceCreator {
        private readonly EventBusPortInMemoryAdapter eventBus;

        public TracksServiceCreator(EventBusPortInMemoryAdapter eventBus) {
            this.eventBus = eventBus;
        }

        public AddTrackToPlayListService CreateAddTrackToPlayListService() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new AddTrackToPlayListService(pLayListPostgreSqlAdapter, eventBus);
        }
        
        public DeleteTrackFromPLayListService CreateDeleteTrackFromPLayListService() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistenceAdapter();
            return new DeleteTrackFromPLayListService(pLayListPostgreSqlAdapter, eventBus);
        }
    }
}