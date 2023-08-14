using System;
using System.Collections.Generic;
using MyMusic.Events;

namespace MyMusic.Application.Write.Ports {
    public interface EventPublisherPort {
        void Publish<T>(List<T> events) where T : Event;
        
        void Register<T>(Action<T> eventConsumer) where T : Event;
    }
}