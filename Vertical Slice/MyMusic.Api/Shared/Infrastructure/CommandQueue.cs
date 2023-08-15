using System;
using LanguageExt;
using MyMusic.PlayLists.Domain.Error;

namespace MyMusic.Shared.Infrastructure {
    public interface CommandQueue {
        void Queue<T>(T command) where T : Command;
        
        void SetQueueSingleConsumer<T>(Func<T, Either<DomainError, CommandResult>> commandProcessor) where T : Command;
    }
}