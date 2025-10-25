using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Error;

namespace MyMusic.Application.CommandHandlers {
    public class CreatePlayListCommandHandler {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiers;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;

        public CreatePlayListCommandHandler(UniqueIdentifiersPort uniqueIdentifiers, PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
            this.uniqueIdentifiers = uniqueIdentifiers;
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, CommandResult> Handle(CreatePlayList command) {
            var newPlayListId = uniqueIdentifiers.GetNewUniqueIdentifier();
            var playList = PlayList.Create(newPlayListId, command.PlayListName);
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return CommandResult.Success;
        }
        
    }
}