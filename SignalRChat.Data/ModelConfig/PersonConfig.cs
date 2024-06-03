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
    public class PersonConfig: BaseEntityConfig<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);
            builder.ToTable("person");
            builder.Property(e => e.Login).HasColumnName("login");
            builder.Property(e => e.PasswordHash).HasColumnName("password_hash");
            builder.Property(e => e.Role).HasColumnName("role");
            builder.Property(e => e.Email).HasColumnName("email");
            builder.Property(x => x.RegistrationDate)
                .HasColumnName("registration_date")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
