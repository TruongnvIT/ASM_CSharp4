using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using C_4_Buoi1_MVC.Models;

namespace C_4_Buoi1_MVC.Data.Configuraions
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill");//Đặt tên bảng
            builder.HasKey(b => b.Id);
            builder.Property(b => b.CreateDate).IsRequired();
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.Status).IsRequired();

            builder.HasOne(u => u.User).WithMany(b => b.Bill).HasForeignKey(fk => fk.UserId);
        }
    }
}
