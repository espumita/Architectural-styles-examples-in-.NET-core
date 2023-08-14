using System;
using LanguageExt;
using MyMusic.Application.Write.Commands;
using MyMusic.Application.Write.Commands.Successes;
using MyMusic.Domain.Error;

namespace MyMusic.Application.Write.Ports {
    public interface ErrorHandlerDecoratorPort {
        void Execute<T>(Func<Command, Either<DomainError, CommandResult>> commandProcessor, T command) where T : Command;
    }
}