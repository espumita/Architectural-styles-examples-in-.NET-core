using System;
using LanguageExt;
using MyMusic.PlayList.Domain.Error;
using MyMusic.Shared.Commands;
using MyMusic.Shared.Commands.Successes;
using MyMusic.Shared.Ports;

namespace MyMusic.Shared.Websockets {
    public class WebsocketErrorHandlerDecorator: ErrorHandlerDecoratorPort {
        private readonly SignalRWebsocketAdapter websocket;

        public WebsocketErrorHandlerDecorator(SignalRWebsocketAdapter websocket) {
            this.websocket = websocket;
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
            await websocket.PushMessageWithErrorToAll(error.GetType().Name, command);
        }

        private async void HandleException(Exception exception, Command command) {
            await websocket.PushMessageWithErrorToAll(exception.GetType().Name, command);
        }
    }
}