using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Data.FluentConfigs.Product
{
    public class Fluent_Product : IEntityTypeConfiguration<Entities.Product.Product>
    {
        public void Configure(EntityTypeBuilder<Entities.Product.Product> builder)
        {
            builder
                .HasKey(h => h.Id);
            builder
                .Property(p => p.EnglishName)
                .IsRequired()
                .HasMaxLength(200);
            builder
                .Property(p => p.PersianName)
                .IsRequired()
                .HasMaxLength(200);
            builder
                .Property(p => p.IsShowInStore)
                .IsRequired();
            builder
                .Property(p => p.ProductReview)
                .HasMaxLength(1500);
            builder
                .Property(p => p.ImageTitle)
                .HasMaxLength(500);
            builder
                .Property(p => p.Price)
                .IsRequired()
                .HasMaxLength(20);
            builder
                .HasOne(h => h.ProductGroup)
                .WithMany(w => w.Products)
                .HasForeignKey(h => h.ProductGroupId);
            builder
                .HasMany(h => h.ProductDetails)
                .WithOne(h => h.Product)
                .HasForeignKey(h => h.ProductId);
            builder
                .HasMany(h => h.ProductPictures)
                .WithOne(w => w.Product)
                .HasForeignKey(h => h.ProductId);
            builder
                .HasMany(h => h.ProductComments)
                .WithOne(w => w.Product)
                .HasForeignKey(h => h.ProductId);

        }
    }
}
