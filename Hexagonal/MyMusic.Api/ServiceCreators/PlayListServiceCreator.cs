using MyMusic.Application.Services;
using MyMusic.Infrastructure.Persistence;

namespace MyMusic.ServiceCreators {

    public class PlayListServiceCreator {
        public PlayListService CreatePlayListService() {
            var playListDatabaseAdapter = new PLayListDatabaseAdapter();
            return new PlayListService(playListDatabaseAdapter);
        }
    }
}