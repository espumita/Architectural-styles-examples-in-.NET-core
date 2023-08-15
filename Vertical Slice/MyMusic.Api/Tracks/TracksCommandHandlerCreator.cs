using MyMusic.PlayList.Features;
using MyMusic.Shared.Ports;
using MyMusic.Tracks.Features;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.Tracks {

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