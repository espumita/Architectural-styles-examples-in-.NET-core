using LanguageExt;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.PlayLists.Features.CreatePLayList {
    public class CreatePlayListCommandHandler {
        
        private readonly UniqueIdentifiers uniqueIdentifiers;
        private readonly PlayListPersistence playListPersistence;
        private readonly EventPublisher eventPublisher;

        public CreatePlayListCommandHandler(UniqueIdentifiers uniqueIdentifiers, PlayListPersistence playListPersistence, EventPublisher eventPublisher) {
            this.uniqueIdentifiers = uniqueIdentifiers;
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(Features.CreatePLayList.CreatePLayList command) {
            var newPlayListId = uniqueIdentifiers.GetNewUniqueIdentifier();
            var playList = Domain.PlayList.Create(newPlayListId, command.PlayListName);
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
        
    }
}