using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.ModelConfig
{
    public class PersonalMessageConfig: BaseEntityMessageConfig<PersonalMessage>
    {
        public override void Configure(EntityTypeBuilder<PersonalMessage> builder)
        {
            base.Configure(builder);
            builder.ToTable("personal_message");
            builder.Property(e => e.RecipientId).HasColumnName("recipient_id");
        }
    }
}
