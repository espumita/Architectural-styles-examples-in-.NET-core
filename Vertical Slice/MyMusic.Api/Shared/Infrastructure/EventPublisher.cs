using System;
using System.Collections.Generic;

namespace MyMusic.Shared.Infrastructure {
    public interface EventPublisher {
        void Publish<T>(List<T> events) where T : Event;
        
        void Register<T>(Action<T> eventConsumer) where T : Event;
    }
}