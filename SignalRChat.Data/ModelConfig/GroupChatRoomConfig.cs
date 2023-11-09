using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.ModelConfig
{
    public class GroupChatRoomConfig: BaseEntityConfig<GroupChatRoom>
    {
        public override void Configure(EntityTypeBuilder<GroupChatRoom> builder)
        {
            base.Configure(builder);
            builder.ToTable("group_chat_room");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.CreatorId).HasColumnName("creator_id");
        }
    }
}
