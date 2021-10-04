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
    public class Fluent_ProductPicture : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.PictureName).IsRequired().HasMaxLength(200);
        }
    }
}
