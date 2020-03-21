using System;
using MyMusic.Application.Ports;

namespace MyMusic.Infrastructure.Adapters {

    public class UniqueIdentifiersInMemoryAdapter : UniqueIdentifiersPort {
        
        public string GetNewUniqueIdentifier() {
            return Guid.NewGuid().ToString();
        }
    }
}