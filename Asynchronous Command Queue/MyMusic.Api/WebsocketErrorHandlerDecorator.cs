using System;
using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.Application.Ports;
using MyMusic.Domain.Error;

namespace MyMusic {
    public class WebsocketErrorHandlerDecorator: ErrorHandlerDecoratorPort {
        private readonly SignalRWebsocketAdapter websocketPort;

        public WebsocketErrorHandlerDecorator(SignalRWebsocketAdapter websocketPort) {
            this.websocketPort = websocketPort;
        }

        public void Execute<T>(Func<Command,Either<DomainError,CommandResult>> commandProcessor, T command) where T : Command {
            try {
                var commandResult = commandProcessor(command);
                commandResult.IfLeft(error => HandleError(error, command));
            } catch (Exception exception) {
                HandleException(exception, command);
            }
        }

        private async void HandleError(DomainError error, Command command) {
            await websocketPort.PushMessageWithErrorToAll(error.GetType().Name, command);
        }

        private async void HandleException(Exception exception, Command command) {
            await websocketPort.PushMessageWithErrorToAll(exception.GetType().Name, command);
        }
    }
}