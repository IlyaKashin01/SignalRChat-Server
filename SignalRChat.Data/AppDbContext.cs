using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Domain.Entities;
using TravelApi.Infrastructure.Data.Extensions;

namespace SignalRChat.Data
{
#nullable disable
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Users { get; set; }
        public DbSet<PersonalMessage> PersonalMessages { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<GroupChatRoom> Groups { get; set; }
        public DbSet<PersonJoinToGroup> PersonJoinToGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddModelConfiguration();
            modelBuilder.AddDeletedQueryFilters();
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}