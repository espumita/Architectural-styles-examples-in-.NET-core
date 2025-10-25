using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters.Http;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class TracksServiceCreator {
        
        public AddTrackToPlayListService CreateAddTrackToPlayListService() {
            var pLayListPostgreSqlAdapter = new PlayListPostgreSQLPersistenceAdapter();
            var tracksNotifierAdapter = new TraksSpotifyApiAdapter();
            return new AddTrackToPlayListService(pLayListPostgreSqlAdapter, tracksNotifierAdapter);
        }
        
        public RemoveTrackFromPlayListService CreateRemoveTrackFromPlayListService() {
            var pLayListPostgreSqlAdapter = new PlayListPostgreSQLPersistenceAdapter();
            var tracksNotifierAdapter = new TraksSpotifyApiAdapter();
            return new RemoveTrackFromPlayListService(pLayListPostgreSqlAdapter, tracksNotifierAdapter);
        }
    }
}