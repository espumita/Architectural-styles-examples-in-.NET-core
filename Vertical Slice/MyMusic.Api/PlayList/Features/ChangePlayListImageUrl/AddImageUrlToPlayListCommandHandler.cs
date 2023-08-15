using LanguageExt;
using MyMusic.PlayList.Domain.Error;
using MyMusic.Shared.Commands.Successes;
using MyMusic.Shared.Ports;

namespace MyMusic.PlayList.Features.ChangePlayListImageUrl {
    public class AddImageUrlToPlayListCommandHandler {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;

        public AddImageUrlToPlayListCommandHandler(PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
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