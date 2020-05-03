using System;
using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.Domain.Error;

namespace MyMusic.Application.Ports {
    public interface ErrorHandlerDecoratorPort {
        void Execute<T>(Func<Command, Either<DomainError, CommandResult>> commandProcessor, T command) where T : Command;
    }
}