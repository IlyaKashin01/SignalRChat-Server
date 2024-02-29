using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.ModelConfig
{
    public class BaseEntityMessageConfig<TEntity>: IEntityTypeConfiguration<TEntity> where TEntity: BaseEntityMessage 
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.SenderId).HasColumnName("sender_id");
            builder.Property(e => e.Content).HasColumnName("content");
            builder.Property(e => e.SentAt).HasColumnName("sent_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedDate).HasColumnName("updated_date");
            builder.Property(x => x.DeleteDate).HasColumnName("delete_date");
            builder.Property(e => e.IsCheck).HasColumnName("is_check");
        }
    }
}
