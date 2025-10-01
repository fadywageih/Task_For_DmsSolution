using Task_For_Dms.DAL.Common.Enums;

namespace Task_For_Dms.BLL.Model
{
    public class DeviceToReturnDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime? AcquisitionDate { get; set; }
        public string Memo { get; set; } = string.Empty;
        public string CategoryName => Category switch
        {
           DeviceCategory.Laptop => "Laptop",
           DeviceCategory.Printer => "Printer",
           DeviceCategory.Switch => "Switch",
            _ => "Unknown"
        };
        public DeviceCategory Category { get; set; }
    }
}
