using Microsoft.AspNetCore.SignalR;

namespace Chat.Common
{
    public class ChatUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
        }
    }
}
