using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Persistence;
using MyMusic.Tracks.Features.AddTrackToPlayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.Tracks {

    public class TracksCommandHandlerCreator {
        private readonly EventPublisher eventPublisher;

        public TracksCommandHandlerCreator(EventPublisher eventPublisher) {
            this.eventPublisher = eventPublisher;
        }

        public AddTrackToPlayListCommandHandler CreateAddTrackToPlayListCommandHandler() {
            var playListPostgreSqlAdapter = new PlayListPostgreSQLPersistence();
            return new AddTrackToPlayListCommandHandler(playListPostgreSqlAdapter, eventPublisher);
        }
        
        public RemoveTrackFromPlayListCommandHandler CreateRemoveTrackFromPlayListCommandHandler() {
            var playListPostgreSqlAdapter = new PlayListPostgreSQLPersistence();
            return new RemoveTrackFromPlayListCommandHandler(playListPostgreSqlAdapter, eventPublisher);
        }
    }
}