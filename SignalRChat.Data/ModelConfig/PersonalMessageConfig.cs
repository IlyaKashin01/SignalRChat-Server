using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.ModelConfig
{
    public class PersonalMessageConfig: BaseEntityConfig<PersonalMessage>
    {
        public override void Configure(EntityTypeBuilder<PersonalMessage> builder)
        {
            base.Configure(builder);
            builder.ToTable("personal_message");
            builder.Property(e => e.SenderId).HasColumnName("sender_id");
            builder.Property(e => e.RecipientId).HasColumnName("recipient_id");
            builder.Property(e => e.Content).HasColumnName("content");
            builder.Property(e => e.SentAt).HasColumnName("sent_at");
            builder.Property(e => e.IsCheck).HasColumnName("is_check");
        }
    }
}
