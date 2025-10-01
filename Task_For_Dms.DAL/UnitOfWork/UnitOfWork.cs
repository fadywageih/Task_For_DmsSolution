using Task_For_Dms.DAL.Presistance;
using Task_For_Dms.DAL.Repositories.Device;

namespace Task_For_Dms.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _applicationDBContext;

       
        public IDeviceRepository DeviceRepository
        {
            get
            {
                return new DeviceRepository(_applicationDBContext);
            }
        }
        public UnitOfWork(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _applicationDBContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _applicationDBContext.DisposeAsync();
        }
    }
}
