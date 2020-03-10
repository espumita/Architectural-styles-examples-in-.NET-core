using System;
using System.Collections.Generic;
using MyMusic.Application.Ports;
using EventHandler = MyMusic.Application.SharedKernel.Model.EventHandler;

namespace MyMusic.Infrastructure.Adapters {

    public class EventBusInMemoryAdapter : EventBus {
        
        private Dictionary<Type, List<EventHandler>> eventsHandlers = new Dictionary<Type, List<EventHandler>>();
        
        public void Raise<T>(T @event) {
            if(eventsHandlers.ContainsKey(typeof(T))) {
                eventsHandlers[typeof(T)].ForEach(handler => handler.Handle(@event));
            }
        }

        public void Register<T>(EventHandler eventHandler) {
            if(eventsHandlers.ContainsKey(typeof(T))) {
                eventsHandlers[typeof(T)].Add(eventHandler);
            } else {
                eventsHandlers[typeof(T)] = new List<EventHandler>{ eventHandler };
            }
        }
    }

}