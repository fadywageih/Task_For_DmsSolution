namespace Task_For_Dms.DAL.Models
{
    public class DeviceProperty : ModelBase
    {
        public int DeviceId { get; set; }
        public string Key { get; set; } = string.Empty; 
        public string Value { get; set; } = string.Empty; 

        public virtual Device Device { get; set; } = null!;
    }
}
