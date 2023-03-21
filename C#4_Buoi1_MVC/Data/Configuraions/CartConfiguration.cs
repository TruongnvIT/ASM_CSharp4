using C_4_Buoi1_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C_4_Buoi1_MVC.Data.Configuraions
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(c => c.IdUser);
            builder.Property(c => c.Description).HasMaxLength(200);

            builder.HasOne(u => u.User).WithOne(c => c.Cart).HasForeignKey<Cart>(fk => fk.IdUser);

        }
    }
}
