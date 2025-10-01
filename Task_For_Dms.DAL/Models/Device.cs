using Task_For_Dms.DAL.Common.Enums;

namespace Task_For_Dms.DAL.Models
{
    public class Device:ModelBase
    {
        public string DeviceName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime? AcquisitionDate { get; set; }
        public string Memo { get; set; } = string.Empty;
        public DeviceCategory Category { get; set; }

        public virtual ICollection<DeviceProperty> Properties { get; set; } = new HashSet<DeviceProperty>();
    }
}
