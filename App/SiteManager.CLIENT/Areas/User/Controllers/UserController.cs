using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Models;
using SiteManager.CLIENT.Services.Abstractions;

namespace SiteManager.CLIENT.Areas.User.Controllers
{
	[Area("User")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [Route("account")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [Route("account/personel-information")]
        [HttpGet]
        public async Task<IActionResult> PersonelInformation()
        {
            var user = await _userService.GetUserAsync();
            if (user == null)
            {
                _logger.LogWarning("User information could not be retrieved.");
                return RedirectToAction("Login", "Login", new { area = "Authentication" });
            }

            _logger.LogInformation("User data sent to view: {@User}", user);
            return View(user);
        }
        [Route("account/addresses")]
        [HttpGet]
        public async Task<IActionResult> Addresses()
        {
	        try
	        {
		        var addresses = await _userService.GetAddressesAsync();
		        if (addresses == null || !addresses.Any())
		        {
			        _logger.LogWarning("Addresses could not be retrieved or no addresses found.");
			        ViewBag.Message = "Adres bulunamadı.";
			        ViewBag.CanAddAddress = true;
			        return View(new List<AddressModel>());
		        }

		        if (addresses.Count >= 5)
		        {
			        _logger.LogWarning("Maximum address limit reached.");
			        ViewBag.ErrorMessage  = "Maksimum adres sınırına ulaşıldı. Yeni adres ekleyemezsiniz.";
			        ViewBag.CanAddAddress = false;
		        }
		        else
		        {
			        ViewBag.CanAddAddress = true;
		        }

		        _logger.LogInformation("Addresses data sent to view: {@Addresses}", addresses);
		        return View(addresses);
	        }
	        catch (Exception ex)
	        {
		        _logger.LogError(ex, "An error occurred while retrieving addresses.");
		        ViewBag.Message = ex.Message;
		        ViewBag.CanAddAddress = false;
		        return View(new List<AddressModel>());
	        }
        }

		[Route("account/addresses/{id}")]
		[HttpGet]
		public async Task<IActionResult> AddressDetails(Guid id)
		{
			var address = await _userService.GetAddressAsync(id);
			if (address == null)
			{
				_logger.LogWarning("Address not found. ID: {Id}", id);
				return View("Error");
			}

			_logger.LogInformation("Address data sent to view: {@Address}", address);
			return View(address);
		}

		[Route("account/addresses/delete/{id}")]
		[HttpPost]
		public async Task<IActionResult> DeleteAddress(Guid id)
		{
			var result = await _userService.DeleteAddressAsync(id);
			if (!result)
			{
				_logger.LogWarning("Failed to delete address. ID: {Id}", id);
				return View("Error");
			}

			_logger.LogInformation("Address deleted successfully. ID: {Id}", id);
			return RedirectToAction("Addresses");
		}
		[Route("account/addresses/add")]
		[HttpGet]
		public IActionResult AddAddress()
		{
			return View();
		}

        [Route("account/addresses/add")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid. Errors: {@ModelStateErrors}", ModelState.Values.SelectMany(v => v.Errors));
                TempData["ErrorMessage"] = "Model validation failed. Please check the input values.";
                return View(model);
            }

            try
            {
                var result = await _userService.AddAddressAsync(model);
                if (!result)
                {
                    _logger.LogWarning("Failed to add address. Address: {@Address}", model);
                    TempData["ErrorMessage"] = "Failed to add address. Please try again later.";
                    return View("Error");
                }

                _logger.LogInformation("Address added successfully. Address: {@Address}", model);
                TempData["SuccessMessage"] = "Address added successfully.";
                return RedirectToAction("Addresses");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding address. Address: {@Address}", model);
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                return View("Error");
            }
        }
        [Route("account/addresses/edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditAddress(Guid id)
        {
            var address = await _userService.GetAddressAsync(id);
            if (address == null)
            {
                _logger.LogWarning("Address not found. ID: {Id}", id);
                return View("Error");
            }

            _logger.LogInformation("Address data sent to view for editing: {@Address}", address);
            return View(address);
        }

        [Route("account/addresses/edit/{id}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditAddress(Guid id, AddressModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid. Errors: {@ModelStateErrors}", ModelState.Values.SelectMany(v => v.Errors));
                TempData["ErrorMessage"] = "Model validation failed. Please check the input values.";
                return View(model);
            }

            try
            {
                var result = await _userService.UpdateAddressAsync(id, model);
                if (!result)
                {
                    _logger.LogWarning("Failed to update address. Address ID: {Id}", id);
                    TempData["ErrorMessage"] = "Failed to update address. Please try again later.";
                    return View("Error");
                }

                _logger.LogInformation("Address updated successfully. Address ID: {Id}", id);
                TempData["SuccessMessage"] = "Address updated successfully.";
                return RedirectToAction("Addresses");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating address. Address ID: {Id}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                return View("Error");
            }
        }
    }
}