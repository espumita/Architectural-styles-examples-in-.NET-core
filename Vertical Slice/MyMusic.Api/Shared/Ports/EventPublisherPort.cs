using System;
using System.Collections.Generic;

namespace MyMusic.Shared.Ports {
    public interface EventPublisherPort {
        void Publish<T>(List<T> events) where T : Event;
        
        void Register<T>(Action<T> eventConsumer) where T : Event;
    }
}