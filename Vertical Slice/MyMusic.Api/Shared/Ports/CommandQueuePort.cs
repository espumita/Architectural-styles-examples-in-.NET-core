using System;
using LanguageExt;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.Shared.Commands;
using MyMusic.Shared.Commands.Successes;

namespace MyMusic.Shared.Ports {
    public interface CommandQueuePort {
        void Queue<T>(T command) where T : Command;
        
        void SetQueueSingleConsumer<T>(Func<T, Either<DomainError, CommandResult>> commandProcessor) where T : Command;
    }
}