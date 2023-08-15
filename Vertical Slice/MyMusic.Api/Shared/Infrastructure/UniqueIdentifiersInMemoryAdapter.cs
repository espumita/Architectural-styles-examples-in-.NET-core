using System;
using MyMusic.Shared.Ports;

namespace MyMusic.Shared.Infrastructure {

    public class UniqueIdentifiersInMemoryAdapter : UniqueIdentifiersPort {
        
        public string GetNewUniqueIdentifier() {
            return Guid.NewGuid().ToString();
        }
    }
}