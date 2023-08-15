using System.Threading.Tasks;
using MyMusic.Shared;
using MyMusic.Shared.Commands;

namespace MyMusic.PlayLists.Features {

    public interface WebsocketPort {
        Task PushMessageWithEventToAll(Event @event);
        Task PushMessageWithErrorToAll(string error, Command command);
    }
}