using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;

namespace MyMusic.Infrastructure.Adapters.Websockets {
    public class SignalRWebsocketAdapter : Hub, WebsocketPort {
        public void PushMessageWithEvent(Event @event) {
            var serializedMessage = JsonSerializer.Serialize(@event);
            Clients.All.SendAsync("ReceiveMessage", "test",serializedMessage);
        }
    }
}