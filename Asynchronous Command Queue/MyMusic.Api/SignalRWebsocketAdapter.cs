using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;

namespace MyMusic {
    public class SignalRWebsocketAdapter : Hub, WebsocketPort {
        public async  Task PushMessageWithEventToAll(Event @event) {
            var serializedMessage = JsonSerializer.Serialize(@event);
            if (Clients != null){
                await Clients.All.SendAsync("AllMyMusicTarget", @event.GetType().Name,@event);
            }
        }
    }
}