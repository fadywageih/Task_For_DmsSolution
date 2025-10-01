namespace Task_For_Dms.BLL.StaticModel
{
    public class SwitchDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public int Ports { get; set; }
        public decimal Speed { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
    }
}
