using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.ModelConfig
{
    public class PersonJoinToGroupConfig: BaseEntityConfig<PersonJoinToGroup>
    {
        public override void Configure(EntityTypeBuilder<PersonJoinToGroup> builder)
        {
            base.Configure(builder);
            builder.ToTable("person_join_to_group");
            builder.Property(e => e.PersonId).HasColumnName("person_id");
            builder.Property(e => e.GroupId).HasColumnName("group_id");
        }
    }
}
