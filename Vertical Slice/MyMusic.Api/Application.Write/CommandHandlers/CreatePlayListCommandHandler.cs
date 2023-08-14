using LanguageExt;
using MyMusic.Application.Write.Commands;
using MyMusic.Application.Write.Commands.Successes;
using MyMusic.Application.Write.Ports;
using MyMusic.Application.Write.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Error;

namespace MyMusic.Application.Write.CommandHandlers {
    public class CreatePlayListCommandHandler {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiers;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;

        public CreatePlayListCommandHandler(UniqueIdentifiersPort uniqueIdentifiers, PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
            this.uniqueIdentifiers = uniqueIdentifiers;
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(CreatePLayList command) {
            var newPlayListId = uniqueIdentifiers.GetNewUniqueIdentifier();
            var playList = PlayList.Create(newPlayListId, command.PlayListName);
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
        
    }
}