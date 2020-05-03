using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Error;

namespace MyMusic.Application.CommandHandlers {
    public class AddTrackToPlayListCommandHandler {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;
        
        public AddTrackToPlayListCommandHandler(PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }
        
        public Either<DomainError, CommandResult> Handle(AddTrackToPLayList command) {
            var playList = playListPersistence.GetPlayList(command.PlaylistId);
            var error = playList.Add(Track.With(command.TrackId));
            if (error.IsSome) return error.ValueUnsafe();
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
        
    }

}