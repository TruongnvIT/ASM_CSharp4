using C_4_Buoi1_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C_4_Buoi1_MVC.Data.Configuraions
{
    public class BillDetailsConfiguration : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.ToTable("BillDetails");
            builder.HasKey(bd => bd.Id);
            builder.Property(bd => bd.Quantity).IsRequired();
            builder.Property(bd => bd.IdHD).IsRequired();
            builder.Property(bd => bd.IdSP).IsRequired();
            builder.Property(bd => bd.Price).IsRequired();

            builder.HasOne(b => b.Bill).WithMany(bd => bd.BillDetails).HasForeignKey(fk => fk.IdHD);
            builder.HasOne(p => p.Product).WithMany(bd => bd.BillDetails).HasForeignKey(fk => fk.IdSP);

        }
    }
}
