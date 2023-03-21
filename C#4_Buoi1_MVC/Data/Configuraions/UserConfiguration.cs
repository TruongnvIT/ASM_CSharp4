using C_4_Buoi1_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C_4_Buoi1_MVC.Data.Configuraions
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(50).IsRequired();
            builder.Property(u => u.IdRole);
            builder.Property(u => u.Status).IsRequired();

            builder.HasOne(r => r.Role).WithMany(u => u.User).HasForeignKey(fk => fk.IdRole);

        }
    }
}
