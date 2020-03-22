using System;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Ports {
    public interface EventBusPort {
        void Raise<T>(T @event) where T : Event;
        
        void Register<T>(Action<T> eventHandler) where T : Event;
    }
}