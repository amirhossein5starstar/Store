using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities.Product;

namespace Store.Data.FluentConfigs.Product
{
    public class Fluent_ProductGroup : IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(20);
        }
    }
}
