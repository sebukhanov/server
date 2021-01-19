using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Common
{
    public class ChatDBContext : DbContext
    {
        public ChatDBContext User { get; set; }
        public ChatDBContext Message { get; set; }
        public ChatDBContext Dialog { get; set; }

        public ChatDBContext(DbContextOptions options)
        : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }

    }
}

