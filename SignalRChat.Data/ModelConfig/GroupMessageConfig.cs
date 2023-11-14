using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.ModelConfig
{
    public class GroupMessageConfig: BaseEntityConfig<GroupMessage>
    {
        public override void Configure(EntityTypeBuilder<GroupMessage> builder)
        {
            base.Configure(builder);
            builder.ToTable("group_message");
            builder.Property(e => e.SenderId).HasColumnName("sender_id");
            builder.Property(e => e.GroupId).HasColumnName("group_id");
            builder.Property(e => e.Content).HasColumnName("content");
            builder.Property(e => e.SentAt).HasColumnName("sent_at");
            builder.Property(e => e.IsCheck).HasColumnName("is_check");
        }
    }
}
