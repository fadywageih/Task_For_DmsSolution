
using Microsoft.EntityFrameworkCore;
using Task_For_Dms.DAL.Presistance;
using Task_For_Dms.DAL.Repositories.Generic;

namespace Task_For_Dms.DAL.Repositories.Device
{
    public class DeviceRepository: GenericRepository<DAL.Models.Device>, IDeviceRepository
    {
        public DeviceRepository(ApplicationDBContext context) : base(context)
        {
        }
        public async Task<IEnumerable<string>> GetDistinctPropertyKeysAsync()
        {
            return await _dbContext.DevicePropertys
                .Where(p => !p.IsDeleted)
                .Select(p => p.Key)
                .Distinct()
                .OrderBy(k => k)
                .ToListAsync();
        }
    }
}
