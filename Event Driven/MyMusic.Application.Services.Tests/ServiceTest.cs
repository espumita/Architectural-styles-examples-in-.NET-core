using System.Collections.Generic;
using System.Linq;
using MyMusic.Application.Ports;
using MyMusic.Domain.Events;
using NSubstitute;

namespace MyMusic.Application.Services.Tests {

    public class ServiceTest {
        
        protected void VerifyEventHasBeenRaised(Event expectedEvent, EventPublisherPort eventPublisher) {
            eventPublisher.Received()
                .Publish(Arg.Is <List<Event>>(events =>
                    events.Single().Equals(expectedEvent)));
        }
    }
}