using System.Threading.Tasks;
using MyMusic.Application.Write.Commands;
using MyMusic.Events;

namespace MyMusic.Application.Write.Ports.Websockets {

    public interface WebsocketPort {
        Task PushMessageWithEventToAll(Event @event);
        Task PushMessageWithErrorToAll(string error, Command command);
    }
}