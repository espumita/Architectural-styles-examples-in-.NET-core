using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.Application.Ports;
using MyMusic.Domain.Error;

namespace MyMusic.Infrastructure.Adapters {
    public class AsynchronousCommandQueueInMemoryAdapter : CommandQueuePort {
        private readonly ErrorHandlerDecoratorPort errorHandlerDecorator;

        public AsynchronousCommandQueueInMemoryAdapter(ErrorHandlerDecoratorPort errorHandlerDecorator) {
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