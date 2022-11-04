using Microsoft.AspNetCore.SignalR;
using MosquittoSub_API.Controllers;
using System;
using System.Threading.Tasks;

namespace MosquittoSub_API.SignalR
{
    public class WebSocketServer : Hub
    {

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");

            var data = new Mosquitto().Get();

            Clients.All.SendAsync("SendValues", data);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Disconnected");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
