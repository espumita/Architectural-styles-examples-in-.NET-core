using System;
using LanguageExt;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.Shared.Commands;
using MyMusic.Shared.Commands.Successes;

namespace MyMusic.Shared.Ports {
    public interface ErrorHandlerDecoratorPort {
        void Execute<T>(Func<Command, Either<DomainError, CommandResult>> commandProcessor, T command) where T : Command;
    }
}