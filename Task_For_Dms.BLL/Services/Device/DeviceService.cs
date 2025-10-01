using Microsoft.EntityFrameworkCore;
using Task_For_Dms.BLL.Model;
using Task_For_Dms.DAL.Models;
using Task_For_Dms.DAL.UnitOfWork;

namespace Task_For_Dms.BLL.Services.Device
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DeviceToReturnDto>> GetAllDevicesAsync(string search)
        {
            var query = _unitOfWork.DeviceRepository.GetAllAsQuerable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(d =>
                    d.DeviceName.Contains(search) ||
                    d.SerialNumber.Contains(search) ||
                    d.Memo.Contains(search));
            }

            var devices = await query
                .Select(d => new DeviceToReturnDto
                {
                    Id = d.Id,
                    DeviceName = d.DeviceName,
                    SerialNumber = d.SerialNumber,
                    AcquisitionDate = d.AcquisitionDate,
                    Memo = d.Memo,
                    Category = d.Category
                })
                .ToListAsync();

            return devices;
        }

        public async Task<DeviceDetailsToReturnDto?> GetDeviceByIdAsync(int id)
        {
            var device = await _unitOfWork.DeviceRepository.GetAllAsQuerable()
                .Include(d => d.Properties)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (device == null) return null;

            return new DeviceDetailsToReturnDto
            {
                Id = device.Id,
                DeviceName = device.DeviceName,
                SerialNumber = device.SerialNumber,
                AcquisitionDate = device.AcquisitionDate,
                Memo = device.Memo,
                Category = device.Category,
                Properties = device.Properties.ToDictionary(p => p.Key, p => p.Value)
            };
        }
        public async Task<int> CreateDeviceAsync(DeviceCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.DeviceName))
                throw new ArgumentException("Device name is required.");

            var device = new DAL.Models.Device
            {
                DeviceName = dto.DeviceName,
                SerialNumber = dto.SerialNumber,
                AcquisitionDate = dto.AcquisitionDate,
                Memo = dto.Memo,
                Category = dto.Category,
            };
            var properties = dto.Properties.Select(prop => new DeviceProperty
            {
                Key = prop.Key,
                Value = prop.Value
            }).ToList();

            device.Properties = properties;

            _unitOfWork.DeviceRepository.Add(device);
            var result = await _unitOfWork.CompleteAsync();
            return device.Id;
        }

        public async Task<int> UpdateDeviceAsync(DeviceUpdateDto dto)
        {
            var existingDevice = await _unitOfWork.DeviceRepository.GetByIdAsync(dto.Id);
            if (existingDevice == null)
                throw new KeyNotFoundException($"Device with ID {dto.Id} not found.");
            existingDevice.DeviceName = dto.DeviceName;
            existingDevice.SerialNumber = dto.SerialNumber;
            existingDevice.AcquisitionDate = dto.AcquisitionDate;
            existingDevice.Memo = dto.Memo;
            existingDevice.Category = dto.Category;
            existingDevice.Properties.Clear();
            foreach (var prop in dto.Properties)
            {
                existingDevice.Properties.Add(new DeviceProperty
                {
                    Key = prop.Key,
                    Value = prop.Value
                });
            }

            _unitOfWork.DeviceRepository.Update(existingDevice);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<string>> GetDistinctPropertyKeysAsync()
        {
            return await _unitOfWork.DeviceRepository.GetDistinctPropertyKeysAsync();
        }
        public async Task<bool> DeletedDeviceAsync(int id)
        {
            var device = await _unitOfWork.DeviceRepository.GetByIdAsync(id);
            if (device == null) return false;

            _unitOfWork.DeviceRepository.Delete(device);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}