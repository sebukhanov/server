using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Chat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Chat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {

        private ChatDBContext dbContext;
        public ChatHub(ChatDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private static Dictionary<string, List<string>> ClientConnections = new Dictionary<string, List<string>>();

        public override async Task OnConnectedAsync()
        {
            var users = dbContext.Users.Select(x => new { x.Name }).ToArray();
            await Clients.Caller.SendAsync("GetUsers", users);

            if (!ClientConnections.ContainsKey($"{Context.User.Identity.Name}"))
            {
                ClientConnections.Add($"{Context.User.Identity.Name}", new List<string> { $"{Context.ConnectionId}" });
                await Clients.Others.SendAsync("UserOnline", Context.User.Identity.Name);
            }
            else
            {
                ClientConnections[$"{Context.User.Identity.Name}"].Add(Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {

            if (ClientConnections.ContainsKey($"{Context.User.Identity.Name}"))
            {
                if (ClientConnections[$"{Context.User.Identity.Name}"].Contains(Context.ConnectionId))
                {
                    ClientConnections[$"{Context.User.Identity.Name}"].Remove(Context.ConnectionId);
                    if (ClientConnections[$"{Context.User.Identity.Name}"].Count == 0)
                    {
                        ClientConnections.Remove($"{Context.User.Identity.Name}");
                        await Clients.Others.SendAsync("UserOffline", Context.User.Identity.Name);
                    }
                }
            }
            await base.OnDisconnectedAsync(exception);
        }
        public async Task GetMessages(string user1, string user2)
        {
            var messages1 = dbContext.Messages.Where(m => m.UserFrom == user1 && m.UserTo == user2);
            var messages2 = dbContext.Messages.Where(m => m.UserFrom == user2 && m.UserTo == user1);
            var messages = messages1.Union(messages2).OrderBy(i => i.Id);
            await Clients.Caller.SendAsync("GetMessages", messages);
        }
        public async Task SendMessage(Message message)
        {
            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
            if (Context.User.Identity.Name != message.UserTo)
                await Clients.User(Context.User.Identity.Name).SendAsync("SendMessage", message);
            await Clients.User(message.UserTo).SendAsync("SendMessage", message);
        }

    }
}
