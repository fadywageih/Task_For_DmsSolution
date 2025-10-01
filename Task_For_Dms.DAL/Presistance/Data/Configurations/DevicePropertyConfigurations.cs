using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_For_Dms.DAL.Models;

namespace Task_For_Dms.DAL.Presistance.Data.Configurations
{
    public class DevicePropertyConfigurations : IEntityTypeConfiguration<DeviceProperty>
    {
        public void Configure(EntityTypeBuilder<DeviceProperty> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Key).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Value).HasMaxLength(500);

            builder.HasOne(p => p.Device)
                   .WithMany(d => d.Properties)
                   .HasForeignKey(p => p.DeviceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
