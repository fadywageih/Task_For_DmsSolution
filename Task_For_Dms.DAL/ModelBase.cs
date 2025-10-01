namespace Task_For_Dms.DAL
{
    public abstract class ModelBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
