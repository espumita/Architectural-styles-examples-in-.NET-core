using System.Threading.Tasks;

namespace MyMusic.Shared.Infrastructure {

    public interface Websocket {
        Task PushMessageWithEventToAll(Event @event);
        Task PushMessageWithErrorToAll(string error, Command command);
    }
}