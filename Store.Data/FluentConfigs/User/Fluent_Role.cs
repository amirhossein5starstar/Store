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
    public class Fluent_Role : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(h => h.RoleId);
            builder
                .Property(p => p.RoleTitle)
                .IsRequired();

            builder.HasData(
                new Role() { RoleId = 1, RoleTitle = "AdminPanel" },
                new Role() { RoleId = 2, RoleTitle = "ManageUser" },
                new Role() { RoleId = 3, RoleTitle = "ManageRoles" },
                new Role() { RoleId = 4, RoleTitle = "ManageProduct" }
            );

        }
    }
}
