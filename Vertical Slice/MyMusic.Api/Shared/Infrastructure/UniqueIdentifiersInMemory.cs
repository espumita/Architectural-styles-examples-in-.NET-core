using System;

namespace MyMusic.Shared.Infrastructure {

    public class UniqueIdentifiersInMemory : UniqueIdentifiers {
        
        public string GetNewUniqueIdentifier() {
            return Guid.NewGuid().ToString();
        }
    }
}