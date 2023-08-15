using LanguageExt;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.PlayLists.Features.ArchivePlayList {
    public class ArchivePlayListCommandHandler {
        
        private readonly PlayListPersistence playListPersistence;
        private readonly EventPublisher eventPublisher;

        public ArchivePlayListCommandHandler(PlayListPersistence playListPersistence, EventPublisher eventPublisher) {
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