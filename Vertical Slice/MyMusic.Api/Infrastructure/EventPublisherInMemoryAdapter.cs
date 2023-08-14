using System;
using System.Collections.Generic;
using MyMusic.Application.Write.Ports;
using MyMusic.Events;

namespace MyMusic.Infrastructure {

    public class EventPublisherInMemoryAdapter : EventPublisherPort {
        
        private Dictionary<Type, List<Action<Event>>> eventConsumers = new Dictionary<Type, List<Action<Event>>>();
        
        public void Publish<T>(List<T> events) where T : Event {
            events.ForEach(@event => {
                if(eventConsumers.ContainsKey(@event.GetType())) {
                    eventConsumers[@event.GetType()].ForEach(eventConsumer => eventConsumer(@event));
                }
            });
        }

        public void Register<T>(Action<T> eventConsumer) where T : Event {
            if(eventConsumers.ContainsKey(typeof(T))) {
                eventConsumers[typeof(T)].Add(@event => eventConsumer((T)@event));
            } else {
                eventConsumers[typeof(T)] = new List<Action<Event>>{ @event => eventConsumer((T)@event) };
            }
        }
    }
}