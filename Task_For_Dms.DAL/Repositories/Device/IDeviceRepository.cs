using Task_For_Dms.DAL.Repositories.Generic;

namespace Task_For_Dms.DAL.Repositories.Device
{
    public interface IDeviceRepository:IGenericRepository<DAL.Models.Device>
    {
        Task<IEnumerable<string>> GetDistinctPropertyKeysAsync();
    }
}
