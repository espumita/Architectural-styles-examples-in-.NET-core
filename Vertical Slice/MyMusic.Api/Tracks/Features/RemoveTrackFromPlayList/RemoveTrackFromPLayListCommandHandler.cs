using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.PlayLists.Features;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.Tracks.Features.RemoveTrackFromPlayList {
    public class RemoveTrackFromPLayListCommandHandler {
        
        private readonly PlayListPersistence playListPersistence;
        private readonly EventPublisher eventPublisher;
        
        public RemoveTrackFromPLayListCommandHandler(PlayListPersistence playListPersistence, EventPublisher eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(Features.RemoveTrackFromPlayList.RemoveTrackFromPlayList command) {
            var playList = playListPersistence.GetPlayList(command.PlaylistId);
            var error = playList.Remove(command.TrackId);
            if (error.IsSome) return error.ValueUnsafe();
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }

    }
}