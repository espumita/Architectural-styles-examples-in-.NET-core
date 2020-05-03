using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MyMusic.Application.Commands;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;

namespace MyMusic.Websockets {
    public class SignalRWebsocketAdapter : Hub, WebsocketPort {
        
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