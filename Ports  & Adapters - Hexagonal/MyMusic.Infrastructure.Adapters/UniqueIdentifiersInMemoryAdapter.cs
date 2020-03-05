using System;
using MyMusic.Application.Ports;

namespace MyMusic.Infrastructure.Adapters {

    public class UniqueIdentifiersInMemoryAdapter : UniqueIdentifiersPort {
        public string GetNewGuid() {
            return Guid.NewGuid().ToString();
        }
    }
}