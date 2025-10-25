using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain.Error;

namespace MyMusic.Application.CommandHandlers {
    public class RemoveTrackFromPlayListCommandHandler {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;
        
        public RemoveTrackFromPlayListCommandHandler(PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(RemoveTrackFromPlayList command) {
            var playList = playListPersistence.GetPlayList(command.PlaylistId);
            var error = playList.Remove(command.TrackId);
            if (error.IsSome) return error.ValueUnsafe();
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }

    }
}