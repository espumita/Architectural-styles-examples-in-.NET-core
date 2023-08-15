using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MyMusic.PlayLists.Features;

namespace MyMusic.Shared.Infrastructure {
    public class SignalRWebsocket : Hub, Websocket {
        
        public async  Task PushMessageWithEventToAll(Event @event) {
            if (Clients != null){
                await Clients.All.SendAsync("AllMyMusicTarget", @event.GetType().Name,@event);
            }
        }

        public async Task PushMessageWithErrorToAll(string error, Command command) {
            if (Clients != null){
                await Clients.All.SendAsync("AllMyMusicTarget", error, command);
            }
        }
    }
}