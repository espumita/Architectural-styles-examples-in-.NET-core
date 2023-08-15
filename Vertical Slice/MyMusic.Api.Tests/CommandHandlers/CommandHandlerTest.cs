using System.Collections.Generic;
using System.Linq;
using MyMusic.Shared;
using MyMusic.Shared.Ports;
using NSubstitute;

namespace MyMusic.Api.Tests.CommandHandlers {

    public class CommandHandlerTest {
        
        protected void VerifyEventHasBeenRaised(Event expectedEvent, EventPublisherPort eventPublisher) {
            eventPublisher.Received()
                .Publish(Arg.Is <List<Event>>(events =>
                    events.Single().Equals(expectedEvent)));
        }
    }
}