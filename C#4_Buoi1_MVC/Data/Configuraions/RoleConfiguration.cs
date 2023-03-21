using C_4_Buoi1_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C_4_Buoi1_MVC.Data.Configuraions
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.RoleName).HasMaxLength(50).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(200);
            builder.Property(r => r.Status).IsRequired();

        }
    }
}
