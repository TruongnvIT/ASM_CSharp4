using C_4_Buoi1_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C_4_Buoi1_MVC.Data.Configuraions
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.AvailbleQuantity).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Supplier).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200);


        }
    }
}
