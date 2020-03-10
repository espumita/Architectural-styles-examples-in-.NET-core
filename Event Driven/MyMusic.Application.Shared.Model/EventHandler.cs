namespace MyMusic.Application.SharedKernel.Model {
    public interface EventHandler {
        void Handle<T>(T @event);
    }
}