using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.PlayLists.Features;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;
using MyMusic.Tracks.Domain;

namespace MyMusic.Tracks.Features.AddTrackToPLayList {
    public class AddTrackToPlayListCommandHandler {
        
        private readonly PlayListPersistence playListPersistence;
        private readonly EventPublisher eventPublisher;
        
        public AddTrackToPlayListCommandHandler(PlayListPersistence playListPersistence, EventPublisher eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }
        
        public Either<DomainError, CommandResult> Handle(Features.AddTrackToPLayList.AddTrackToPLayList command) {
            var playList = playListPersistence.GetPlayList(command.PlaylistId);
            var error = playList.Add(Track.With(command.TrackId));
            if (error.IsSome) return error.ValueUnsafe();
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
        
    }

}