using Task_For_Dms.BLL.Model;

namespace Task_For_Dms.BLL.Services.Device
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceToReturnDto>> GetAllDevicesAsync(string search);
        Task<DeviceDetailsToReturnDto?> GetDeviceByIdAsync(int id);
        Task<int> CreateDeviceAsync(DeviceCreateDto DeviceDto);
        Task<int> UpdateDeviceAsync(DeviceUpdateDto Device);
        Task<IEnumerable<string>> GetDistinctPropertyKeysAsync(); 
        Task<bool> DeletedDeviceAsync(int id);
    }
}
