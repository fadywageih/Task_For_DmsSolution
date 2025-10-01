using System.ComponentModel.DataAnnotations;
using Task_For_Dms.DAL.Common.Enums;

namespace Task_For_Dms.BLL.Model
{
    public class DeviceCreateDto
    {
        [Required(ErrorMessage = "Device name is required.")]
        [StringLength(200, ErrorMessage = "Device name cannot exceed 200 characters.")]
        public string DeviceName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Serial number is required.")]
        [StringLength(100, ErrorMessage = "Serial number cannot exceed 100 characters.")]
        public string SerialNumber { get; set; } = string.Empty;

        public DateTime? AcquisitionDate { get; set; }

        [StringLength(1000, ErrorMessage = "Memo cannot exceed 1000 characters.")]
        public string Memo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a device category.")]
        public DeviceCategory Category { get; set; }

        public Dictionary<string, string> Properties { get; set; } = new();
    }
}