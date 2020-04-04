using System.Collections.Generic;
using System.Linq;
using MyMusic.Application.Ports;
using MyMusic.Domain.Events;
using NSubstitute;

namespace MyMusic.Application.Services.Tests {

    public class ServiceTest {
        
        protected void VerifyEventHasBeenRaised(Event expectedEvent, EventBusPort eventBus) {
            eventBus.Received()
                .Raise(Arg.Is <List<Event>>(events =>
                    events.Single().Equals(expectedEvent)));
        }
    }
}