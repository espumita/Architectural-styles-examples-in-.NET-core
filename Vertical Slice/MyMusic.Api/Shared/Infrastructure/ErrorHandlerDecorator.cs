using System;
using LanguageExt;
using MyMusic.Shared.Domain.Error;

namespace MyMusic.Shared.Infrastructure {
    public interface ErrorHandlerDecorator {
        void Execute<T>(Func<Command, Either<DomainError, CommandResult>> commandProcessor, T command) where T : Command;
    }
}