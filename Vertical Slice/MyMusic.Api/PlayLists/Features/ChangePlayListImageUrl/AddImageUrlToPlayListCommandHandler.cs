using LanguageExt;
using MyMusic.Shared;
using MyMusic.Shared.Domain.Error;
using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Persistence;

namespace MyMusic.PlayLists.Features.ChangePlayListImageUrl {
    public class AddImageUrlToPlayListCommandHandler {
        
        private readonly PlayListPersistence playListPersistence;
        private readonly EventPublisher eventPublisher;

        public AddImageUrlToPlayListCommandHandler(PlayListPersistence playListPersistence, EventPublisher eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(Features.ChangePlayListImageUrl.ChangePlayListImageUrl command) {
            var playList = playListPersistence.GetPlayList(command.PlaylistId);
            playList.AddImageUrl(command.NewImageUrl);

            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
    }
}