using System.Collections.Generic;
using System.Linq;
using MyMusic.Application.Ports;
using MyMusic.Domain.Events;
using NSubstitute;

namespace MyMusic.Application.CommandHandlers.Tests {

    public class CommandHandlerTest {
        
        protected void VerifyEventHasBeenRaised(Event expectedEvent, EventPublisherPort eventPublisher) {
            eventPublisher.Received()
                .Publish(Arg.Is <List<Event>>(events =>
                    events.Single().Equals(expectedEvent)));
        }
    }
}