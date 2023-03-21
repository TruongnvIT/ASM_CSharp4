using C_4_Buoi1_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C_4_Buoi1_MVC.Data.Configuraions
{
    public class CartDetailsConfiguration : IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {
            builder.ToTable("CartDetails");
            builder.HasKey(cd => cd.Id);
            builder.Property(cd => cd.Quantity).IsRequired();

            builder.HasOne(c => c.Cart).WithMany(cd => cd.CartDetails).HasForeignKey(fk => fk.IdUser);
            builder.HasOne(c => c.Product).WithMany(cd => cd.CartDetails).HasForeignKey(fk => fk.IdSP);
        }
    }
}
