using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities.User;

namespace Store.Data.FluentConfigs.User
{
   public class Fluent_UserRole: IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasKey(h => h.UR_Id);
            builder
                .HasOne(h => h.User)
                .WithMany(h => h.UserRoles)
                .HasForeignKey(h => h.UserId);
            builder
                .HasOne(h => h.Role)
                .WithMany(h => h.UserRoles)
                .HasForeignKey(h => h.RoleId);
        }
    }
}
