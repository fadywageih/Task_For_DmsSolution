namespace Task_For_Dms.BLL.StaticModel
{
    public class LaptopDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string HD { get; set; } = string.Empty;
        public int Ram { get; set; }
        public string Display { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string CurrentUser { get; set; } = string.Empty;
    }
}
