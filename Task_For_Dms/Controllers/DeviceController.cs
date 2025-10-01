// Task_For_Dms.Controllers.DeviceController
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task_For_Dms.BLL.Model;
using Task_For_Dms.BLL.Services.Device;
using Task_For_Dms.DAL.Common.Enums;

namespace Task_For_Dms.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<DeviceController> _logger;

        public DeviceController(IDeviceService deviceService, IWebHostEnvironment environment, ILogger<DeviceController> logger)
        {
            _deviceService = deviceService;
            _environment = environment;
            _logger = logger;
        }
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var devices = await _deviceService.GetAllDevicesAsync(search);
            return View(devices);
        }
        #endregion
        #region Create
        #region Get
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = Enum.GetValues<DeviceCategory>()
                                     .Select(e => new SelectListItem
                                     {
                                         Value = ((int)e).ToString(),
                                         Text = e.ToString()
                                     });
            return View();
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<IActionResult> Create(DeviceCreateDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var message = string.Empty;
            try
            {
                var result = await _deviceService.CreateDeviceAsync(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "sorry the department has not been created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //1- log the exception
                _logger.LogError(ex, ex.Message);
                //2- set frindly message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(model);
                }
                else
                {
                    message = "sorry, we are facing a problem please try again later";
                    return View("Error", message);
                }
            }
        }
        #endregion
        #endregion
        #region View
        public async Task<IActionResult> Details(int id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            if (device == null) return NotFound();
            return View(device);
        }
        #endregion
        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            if (device == null) return NotFound();
            var model = new DeviceUpdateDto
            {
                Id = device.Id,
                DeviceName = device.DeviceName,
                SerialNumber = device.SerialNumber,
                AcquisitionDate = device.AcquisitionDate,
                Memo = device.Memo,
                Category = device.Category,
                Properties = device.Properties
            };
            ViewBag.Categories = Enum.GetValues<DeviceCategory>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DeviceUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = Enum.GetValues<DeviceCategory>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    });
                return View(model);
            }
            try
            {
                var result = await _deviceService.UpdateDeviceAsync(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update the device.");
                    ViewBag.Categories = Enum.GetValues<DeviceCategory>()
                        .Select(e => new SelectListItem
                        {
                            Value = ((int)e).ToString(),
                            Text = e.ToString()
                        });
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating device with ID {Id}", model.Id);
                ModelState.AddModelError(string.Empty, _environment.IsDevelopment() ? ex.Message : "An error occurred while updating the device.");

                ViewBag.Categories = Enum.GetValues<DeviceCategory>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    });
                return View(model);
            }
        }

        #endregion
        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            if (device == null) return NotFound();
            return View(device);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _deviceService.DeletedDeviceAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Device deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Device not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting device with ID {Id}", id);
                TempData["ErrorMessage"] = _environment.IsDevelopment()
                    ? ex.Message
                    : "An error occurred while deleting the device.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        public async Task<IActionResult> PropertyItems()
        {
            var uniqueKeys = await _deviceService.GetDistinctPropertyKeysAsync();

            var propertyItems = uniqueKeys
                .Select((key, index) => new PropertyItemDto
                {
                    Id = index + 1,
                    PropertyDescription = key
                })
                .ToList();

            return View(propertyItems);
        }
    }
}