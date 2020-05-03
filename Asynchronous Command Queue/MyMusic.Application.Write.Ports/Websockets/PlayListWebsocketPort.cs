using System.Threading.Tasks;
using MyMusic.Application.Commands;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Ports.Websockets {

    public interface WebsocketPort {
        Task PushMessageWithEventToAll(Event @event);
        Task PushMessageWithErrorToAll(string error, Command command);
    }
}