using System;
using System.Collections.Generic;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Ports {
    public interface EventPublisherPort {
        void Publish<T>(List<T> events) where T : Event;
        
        void Register<T>(Action<T> eventConsumer) where T : Event;
    }
}