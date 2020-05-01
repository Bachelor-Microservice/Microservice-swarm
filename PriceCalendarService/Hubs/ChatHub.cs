using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PriceCalendarService.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await Task.Delay(5000);
            await Clients.All.SendAsync("Send", message).ConfigureAwait(false);
        }
    }
}