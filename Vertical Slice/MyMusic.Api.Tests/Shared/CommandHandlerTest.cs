using System.Collections.Generic;
using System.Linq;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;
using NSubstitute;

namespace MyMusic.Api.Tests.Shared {

    public class CommandHandlerTest {
        
        protected void VerifyEventHasBeenRaised(Event expectedEvent, EventPublisher eventPublisher) {
            eventPublisher.Received()
                .Publish(Arg.Is <List<Event>>(events =>
                    events.Single().Equals(expectedEvent)));
        }
    }
}