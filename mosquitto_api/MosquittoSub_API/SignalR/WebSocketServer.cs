using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MosquittoSub_API.SignalR
{
    public class WebSocketServer : Hub
    {
        private readonly IMem _mem;
        public WebSocketServer(IMem mem)
        {
            _mem = mem;
        }

        public override Task OnConnectedAsync()
        {
            int count = default;
            Console.WriteLine("Connected");

            try
            {
                while(true)
                {
                    var controller = new Controllers.Mosquitto(_mem);
                    var response = controller.Get();

                    Clients.All.SendAsync("Count", count);
                    ++count;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(count);
            }

            Clients.All.SendAsync("Count", count);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Disconnected");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
