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
    public class Fluent_Role: IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(h => h.RoleId);
            builder
                .Property(p => p.RoleTitle)
                .IsRequired();
        }
    }
}
