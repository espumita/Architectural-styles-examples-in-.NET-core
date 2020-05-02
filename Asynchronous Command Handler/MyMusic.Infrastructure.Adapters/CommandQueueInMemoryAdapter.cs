using System;
using System.Collections.Generic;
using MyMusic.Application.Commands;
using MyMusic.Application.Ports;

namespace MyMusic.Infrastructure.Adapters {
    public class CommandQueueInMemoryAdapter : CommandQueuePort {
        
        private Dictionary<Type, Action<Command>> commandProcessors = new Dictionary<Type, Action<Command>>();
        
        public void Queue<T>(T command) where T : Command {
            if(commandProcessors.ContainsKey(typeof(T))) {
                commandProcessors[typeof(T)](command);
            }
        }
        
        public void SetQueueSingleConsumer<T>(Action<T> commandProcessor) where T : Command {
            if(commandProcessors.ContainsKey(typeof(T))) {
                commandProcessors[typeof(T)] = command => commandProcessor((T)command);
            } 
        }
        
    }
}