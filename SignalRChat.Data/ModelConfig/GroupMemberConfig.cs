using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.ModelConfig
{
    public class GroupMemberConfig: BaseEntityConfig<GroupMember>
    {
        public override void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            base.Configure(builder);
            builder.ToTable("group_member");
            builder.Property(e => e.GroupId).HasColumnName("group_id");
            builder.Property(e => e.AddedByPerson).HasColumnName("added_by_person");
            builder.Property(e => e.AddedDate).HasColumnName("added_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(e => e.PersonId).HasColumnName("member_id");
            builder.Property(e => e.IsLeaved).HasColumnName("is_leaved");
        }
    }
}
