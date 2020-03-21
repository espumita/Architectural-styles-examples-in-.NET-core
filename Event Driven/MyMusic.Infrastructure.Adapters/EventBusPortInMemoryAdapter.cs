using System;
using System.Collections.Generic;
using MyMusic.Application.Ports;
using MyMusic.Domain.Events;

namespace MyMusic.Infrastructure.Adapters {

    public class EventBusPortInMemoryAdapter : EventBusPort {
        
        private Dictionary<Type, List<Action<Event>>> eventHandlers = new Dictionary<Type, List<Action<Event>>>();
        
        public void Raise<T>(T @event) where T : Event {
            if(eventHandlers.ContainsKey(typeof(T))) {
                eventHandlers[typeof(T)].ForEach(eventHandler => eventHandler(@event));
            }
        }

        public void Register<T>(Action<T> eventHandler) where T : Event {
            if(eventHandlers.ContainsKey(typeof(T))) {
                eventHandlers[typeof(T)].Add(@event => eventHandler((T)@event));
            } else {
                eventHandlers[typeof(T)] = new List<Action<Event>>{ @event => eventHandler((T)@event) };
            }
        }
    }
}