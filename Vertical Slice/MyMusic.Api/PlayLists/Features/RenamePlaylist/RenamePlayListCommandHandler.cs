using LanguageExt;
using MyMusic.Shared;
using MyMusic.Shared.Domain.Error;
using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Persistence;

namespace MyMusic.PlayLists.Features.RenamePlaylist {
    public class RenamePlayListCommandHandler {
        
        private readonly PlayListPersistence playListPersistence;
        private readonly EventPublisher eventPublisher;

        public RenamePlayListCommandHandler(PlayListPersistence playListPersistence, EventPublisher eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(Features.RenamePlaylist.RenamePlaylist command) {
            var playList = playListPersistence.GetPlayList(command.PlaylistId);
            playList.Rename(command.NewPlayListName);
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
    }
}