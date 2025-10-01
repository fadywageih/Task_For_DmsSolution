using Task_For_Dms.DAL.Repositories.Device;

namespace Task_For_Dms.DAL.UnitOfWork
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        public IDeviceRepository DeviceRepository { get; }
        Task<int> CompleteAsync();
    }
}
