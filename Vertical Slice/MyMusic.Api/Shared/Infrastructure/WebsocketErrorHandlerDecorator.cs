using System;
using LanguageExt;
using MyMusic.PlayLists.Domain.Error;

namespace MyMusic.Shared.Infrastructure {
    public class WebsocketErrorHandlerDecorator: ErrorHandlerDecorator {
        private readonly SignalRWebsocket websocket;

        public WebsocketErrorHandlerDecorator(SignalRWebsocket websocket) {
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