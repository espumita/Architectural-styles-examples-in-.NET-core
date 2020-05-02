using MyMusic.Domain.Events;

namespace MyMusic.Application.Ports.Websockets {

    public interface WebsocketPort {
        void PushMessageWithEvent(Event @event);
    }
}