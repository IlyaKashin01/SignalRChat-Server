using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.ModelConfig
{
    public class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDate)
                .HasColumnName("created_date")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedDate)
                .HasColumnName("updated_date")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.DeleteDate).HasColumnName("delete_date");
        }
    }
}
