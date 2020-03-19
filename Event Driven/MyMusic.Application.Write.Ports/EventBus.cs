using MyMusic.Application.SharedKernel.Model;

namespace MyMusic.Application.Ports {
    public interface EventBus {
        void Raise<T>(T @event) where T : Event;
    }
}