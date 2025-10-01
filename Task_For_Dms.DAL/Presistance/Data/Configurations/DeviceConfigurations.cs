using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_For_Dms.DAL.Models;

namespace Task_For_Dms.DAL.Presistance.Data.Configurations
{
    public class DeviceConfigurations : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.DeviceName).IsRequired().HasMaxLength(200);
            builder.Property(d => d.SerialNumber).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Memo).HasMaxLength(1000);
            builder.Property(d => d.Category).IsRequired();

            builder.HasMany(d => d.Properties)
                   .WithOne(p => p.Device)
                   .HasForeignKey(p => p.DeviceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
