using LanguageExt;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.Shared.Commands.Successes;
using MyMusic.Shared.Ports;

namespace MyMusic.PlayLists.Features.CreatePLayList {
    public class CreatePlayListCommandHandler {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiers;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;

        public CreatePlayListCommandHandler(UniqueIdentifiersPort uniqueIdentifiers, PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
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