using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.ModelConfig
{
    public class MessageJoinToGroupConfig: BaseEntityConfig<MessageJoinToGroup>
    {
        public override void Configure(EntityTypeBuilder<MessageJoinToGroup> builder)
        {
            base.Configure(builder);
            builder.ToTable("message_join_to_group");
            builder.Property(e => e.MessageId).HasColumnName("message_id");
            builder.Property(e => e.GroupId).HasColumnName("group_id");
        }
    }
}
