using System;
using System.Collections.Generic;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Ports {
    public interface EventBusPort {
        void Raise<T>(List<T> events) where T : Event;
        
        void Register<T>(Action<T> eventHandler) where T : Event;
    }
}