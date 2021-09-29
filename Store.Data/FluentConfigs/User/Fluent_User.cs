using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Store.Data.FluentConfigs.User
{
    public class Fluent_User : IEntityTypeConfiguration<Entities.User.User>
    {
        public void Configure(EntityTypeBuilder<Entities.User.User> builder)
        {
            builder
                .HasKey(h => h.UserId);
            builder
                .Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(200);
            builder
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder
                .Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(200);
            builder
                .Property(p => p.ActiveCode)
                .HasMaxLength(500);
            builder
                .Property(p => p.UserAvatar)
                .HasMaxLength(200);
           

        }
    }
}
