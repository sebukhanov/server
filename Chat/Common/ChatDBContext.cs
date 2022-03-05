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
        { 
            Database.EnsureCreated();
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User[] 
            {
                new User { Id=1, Name="Tom", Password="123456"},
                new User { Id=2, Name="Ann", Password="123456"},
                new User { Id=3, Name="Sam", Password="123456"}
            });

        modelBuilder.Entity<Message>().HasData(
            new Message[] 
            {
                new Message { Id=1, UserFrom="Tom", UserTo="Ann", Text="Hi, Ann!", IsRead=false},
                new Message { Id=2, UserFrom="Ann", UserTo="Tom", Text="Hello, Tom!", IsRead=false},
                new Message { Id=3, UserFrom="Tom", UserTo="Ann", Text="How are you?", IsRead=false},
                new Message { Id=4, UserFrom="Ann", UserTo="Tom", Text="I'm fine, And you?", IsRead=false},
                new Message { Id=5, UserFrom="Tom", UserTo="Ann", Text="I'm fine too.", IsRead=false}
            });
    }

    }
}
