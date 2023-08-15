using LanguageExt;
using MyMusic.PlayList.Domain.Error;
using MyMusic.Shared.Commands.Successes;
using MyMusic.Shared.Ports;

namespace MyMusic.PlayList.Features.RenamePlaylist {
    public class RenamePlayListCommandHandler {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;

        public RenamePlayListCommandHandler(PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
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