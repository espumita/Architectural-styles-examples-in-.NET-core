using LanguageExt;
using MyMusic.PlayList.Domain.Error;
using MyMusic.Shared.Commands.Successes;
using MyMusic.Shared.Ports;

namespace MyMusic.PlayList.Features.ArchivePlayList {
    public class ArchivePlayListCommandHandler {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;

        public ArchivePlayListCommandHandler(PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(Features.ArchivePlayList.ArchivePlayList command) {
            var playList = playListPersistence.GetPlayList(command.PlaylistId);
            playList.Archive();
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
    }
}