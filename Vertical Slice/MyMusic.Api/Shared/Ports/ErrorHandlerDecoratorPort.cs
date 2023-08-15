using System;
using LanguageExt;
using MyMusic.PlayList.Domain.Error;
using MyMusic.Shared.Commands;
using MyMusic.Shared.Commands.Successes;

namespace MyMusic.Shared.Ports {
    public interface ErrorHandlerDecoratorPort {
        void Execute<T>(Func<Command, Either<DomainError, CommandResult>> commandProcessor, T command) where T : Command;
    }
}