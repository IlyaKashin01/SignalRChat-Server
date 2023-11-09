using Microsoft.EntityFrameworkCore;
using SignalRChat.Data.ModelConfig;
using SignalRChat.Domain.Entities;
using System.Security.Principal;

namespace TravelApi.Infrastructure.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder AddModelConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonalMessageConfig());
            modelBuilder.ApplyConfiguration(new GroupMessageConfig());
            modelBuilder.ApplyConfiguration(new GroupChatRoomConfig());
            modelBuilder.ApplyConfiguration(new MessageJoinToGroupConfig());
            modelBuilder.ApplyConfiguration(new PersonConfig());
            modelBuilder.ApplyConfiguration(new PersonJoinToGroupConfig());

            return modelBuilder;
        }

        public static ModelBuilder AddDeletedQueryFilters(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalMessage>().HasQueryFilter(e => e.DeleteDate == null);
            modelBuilder.Entity<GroupMessage>().HasQueryFilter(e => e.DeleteDate == null);
            modelBuilder.Entity<GroupChatRoom>().HasQueryFilter(e => e.DeleteDate == null);
            modelBuilder.Entity<MessageJoinToGroup>().HasQueryFilter(e => e.DeleteDate == null);
            modelBuilder.Entity<Person>().HasQueryFilter(e => e.DeleteDate == null);
            modelBuilder.Entity<PersonJoinToGroup>().HasQueryFilter(e => e.DeleteDate == null);

            return modelBuilder;
        }
    }
}
