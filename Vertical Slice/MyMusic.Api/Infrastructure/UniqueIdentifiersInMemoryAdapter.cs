using System;
using MyMusic.Application.Write.Ports;

namespace MyMusic.Infrastructure {

    public class UniqueIdentifiersInMemoryAdapter : UniqueIdentifiersPort {
        
        public string GetNewUniqueIdentifier() {
            return Guid.NewGuid().ToString();
        }
    }
}