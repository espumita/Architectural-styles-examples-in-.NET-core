using System;
using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.Domain.Error;

namespace MyMusic.Application.Ports {
    public interface CommandQueuePort {
        void Queue<T>(T command) where T : Command;
        
        void SetQueueSingleConsumer<T>(Func<T, Either<DomainError, CommandResult>> commandProcessor) where T : Command;
    }
}