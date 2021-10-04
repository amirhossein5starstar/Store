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
    public class Fluent_ProductComment : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder
                .HasKey(h => h.Id);
            builder
                .Property(p => p.ProductId)
                .IsRequired();
            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

        }
    }
}
