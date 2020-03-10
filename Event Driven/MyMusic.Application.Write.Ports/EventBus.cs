namespace MyMusic.Application.Ports {
    public interface EventBus {
        void Raise<T>(T @event);
    }
}