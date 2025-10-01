namespace Task_For_Dms.BLL.StaticModel
{
    public class PrinterDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public bool IsColor { get; set; }
    }
}
