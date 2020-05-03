using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;

namespace MyMusic {
    public class SignalRWebsocketAdapter : Hub, WebsocketPort {
        public async Task PushMessageWithEvent(Event @event) {
            var serializedMessage = JsonSerializer.Serialize(@event);
            await Clients.All.SendAsync("ReceiveMessage", "test",serializedMessage);
        }
    }
}