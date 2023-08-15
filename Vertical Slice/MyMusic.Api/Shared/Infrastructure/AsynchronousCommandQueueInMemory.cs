using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using MyMusic.PlayLists.Domain.Error;

namespace MyMusic.Shared.Infrastructure {
    public class AsynchronousCommandQueueInMemory : CommandQueue {
        private readonly ErrorHandlerDecorator errorHandlerDecorator;

        public AsynchronousCommandQueueInMemory(ErrorHandlerDecorator errorHandlerDecorator) {
            this.errorHandlerDecorator = errorHandlerDecorator;
        }

        private Dictionary<Type, Func<Command, Either<DomainError, CommandResult>>> commandProcessors = new Dictionary<Type, Func<Command, Either<DomainError, CommandResult>>>();
        
        public void Queue<T>(T command) where T : Command {
            if(commandProcessors.ContainsKey(typeof(T))) {
              Task.Run(() => {
                  errorHandlerDecorator.Execute(commandProcessors[typeof(T)],command);
              });
            }
        }
        
        public void SetQueueSingleConsumer<T>(Func<T, Either<DomainError, CommandResult>> commandProcessor) where T : Command {
            if(!commandProcessors.ContainsKey(typeof(T))) {
                commandProcessors[typeof(T)] = command => commandProcessor((T) command);
            } 
        }
        
    }
}