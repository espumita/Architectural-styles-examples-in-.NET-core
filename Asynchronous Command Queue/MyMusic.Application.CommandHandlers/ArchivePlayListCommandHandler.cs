using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain.Error;

namespace MyMusic.Application.CommandHandlers {
    public class ArchivePlayListCommandHandler {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;

        public ArchivePlayListCommandHandler(PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(ArchivePlayList command) {
            var playList = playListPersistence.GetPlayList(command.PlaylistId);
            playList.Archive();
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
    }
}