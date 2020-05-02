using System;
using MyMusic.Application.Commands;

namespace MyMusic.Application.Ports {
    public interface CommandQueuePort {
        void Queue<T>(T command) where T : Command;
        
        void SetQueueSingleConsumer<T>(Action<T> commandProcessor) where T : Command;
    }
}