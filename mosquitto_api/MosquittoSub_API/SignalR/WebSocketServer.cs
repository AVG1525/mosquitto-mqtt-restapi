using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MosquittoSub_API.SignalR
{
    public class WebSocketServer : Hub
    {

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Disconnected");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
