using Authentication.API.Data;
using Authentication.API.Helpers;
using Authentication.LIB.Entities;
using Authentication.LIB.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Authentication.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AddressController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public AddressController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender, ApplicationDbContext context, IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_context = context;
			_configuration = configuration;
		}
		[HttpPost("addresses")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddAddress([FromBody] AddressModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userEmail = User.FindFirstValue(ClaimTypes.Name);
			if (string.IsNullOrEmpty(userEmail))
			{
				return Unauthorized("User is not logged in.");
			}

			var user = await _userManager.FindByEmailAsync(userEmail);
			if (user == null)
			{
				return NotFound("User not found.");
			}
			var existingAddresses = await _context.Addresses.Where(a => a.AppUserId == user.Id).ToListAsync();
			if (existingAddresses.Count >= 5)
			{
				return BadRequest("Adres eklenme sınırına ulaştınız.");
			}
			var address = new Address
			{
				AppUserId = user.Id,
				AddressName = model.AddressName,
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.PhoneNumber,
				AlternativePhoneNumber = model.AlternativePhoneNumber,
				Email = model.Email,
				City = model.City,
				District = model.District,
				Neighborhood = model.Neighborhood,
				AddressInfo = model.AddressInfo,
				IsInvoiceAddress = model.IsInvoiceAddress,
				InvoiceAddressName = model.InvoiceAddressName,
				InvoiceFirstName = model.InvoiceFirstName,
				InvoiceLastName = model.InvoiceLastName,
				InvoicePhoneNumber = model.InvoicePhoneNumber,
				InvoiceEmail = model.InvoiceEmail,
				InvoiceCity = model.InvoiceCity,
				InvoiceDistrict = model.InvoiceDistrict,
				InvoiceNeighborhood = model.InvoiceNeighborhood,
				InvoiceAddressInfo = model.InvoiceAddressInfo,
				TCNumber = model.TCNumber,
				PassaportNumber = model.PassaportNumber,
				PassaportCountry = model.PassaportCountry,
				CommercialTitle = model.CommercialTitle,
				TaxAdministration = model.TaxAdministration,
				TaxNumber = model.TaxNumber,
				InvoiceType = model.InvoiceType,
				InvoicePayer = model.InvoicePayer
			};

			_context.Addresses.Add(address);
			await _context.SaveChangesAsync();
			
			return Ok(new { Message = "Address added successfully." });
		}

		[HttpGet("addresses")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetAddresses()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Name);
			if (string.IsNullOrEmpty(userEmail))
				return Unauthorized("User is not logged in.");

			var user = await _userManager.FindByEmailAsync(userEmail);
			if (user == null)
				return NotFound("User not found");

			var addresses = await _context.Addresses
				.Where(a => a.AppUserId == user.Id && !a.IsDeleted)
				.Select(a => new AddressDTO
				{
					Id = a.Id,
					AddressName = a.AddressName,
					FirstName = a.FirstName,
					LastName = a.LastName,
					PhoneNumber = a.PhoneNumber,
					AlternativePhoneNumber = a.AlternativePhoneNumber,
					Email = a.Email,
					City = a.City,
					District = a.District,
					Neighborhood = a.Neighborhood,
					AddressInfo = a.AddressInfo,
					IsInvoiceAddress = a.IsInvoiceAddress,
					InvoiceAddressName = a.InvoiceAddressName,
					InvoiceFirstName = a.InvoiceFirstName,
					InvoiceLastName = a.InvoiceLastName,
					InvoicePhoneNumber = a.InvoicePhoneNumber,
					InvoiceEmail = a.InvoiceEmail,
					InvoiceCity = a.InvoiceCity,
					InvoiceDistrict = a.InvoiceDistrict,
					InvoiceNeighborhood = a.InvoiceNeighborhood,
					InvoiceAddressInfo = a.InvoiceAddressInfo,
					TCNumber = a.TCNumber,
					PassaportNumber = a.PassaportNumber,
					PassaportCountry = a.PassaportCountry,
					CommercialTitle = a.CommercialTitle,
					TaxAdministration = a.TaxAdministration,
					TaxNumber = a.TaxNumber,
					InvoiceType = a.InvoiceType,
					InvoicePayer = a.InvoicePayer
				})
				.ToListAsync();

			if (!addresses.Any())
				return Ok(new { Message = "No addresses found." });

			return Ok(addresses);
		}


		[HttpGet("addresses/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetAddress(Guid id)
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Name);
			if (string.IsNullOrEmpty(userEmail))
				return Unauthorized("User is not logged in.");

			var user = await _userManager.FindByEmailAsync(userEmail);
			if (user == null)
				return NotFound("User not found");

			var address = await _context.Addresses
				.FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == user.Id && !a.IsDeleted);

			if (address == null)
				return NotFound("Address not found");

			return Ok(address);
		}

		[HttpDelete("addresses/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> DeleteAddress(Guid id)
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Name);
			if (string.IsNullOrEmpty(userEmail))
				return Unauthorized("User is not logged in.");

			var user = await _userManager.FindByEmailAsync(userEmail);
			if (user == null)
				return NotFound("User not found");

			var address = await _context.Addresses
				.FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == user.Id);

			if (address == null)
				return NotFound("Address not found");

			address.IsDeleted = true;

			_context.Addresses.Update(address);
			await _context.SaveChangesAsync();

			return Ok(new { Message = "Address deleted successfully." });
		}
        [HttpPut("addresses/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] AddressModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in.");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == user.Id && !a.IsDeleted);

            if (address == null)
            {
                return NotFound("Address not found.");
            }

            address.AddressName = model.AddressName;
            address.FirstName = model.FirstName;
            address.LastName = model.LastName;
            address.PhoneNumber = model.PhoneNumber;
            address.AlternativePhoneNumber = model.AlternativePhoneNumber;
            address.Email = model.Email;
            address.City = model.City;
            address.District = model.District;
            address.Neighborhood = model.Neighborhood;
            address.AddressInfo = model.AddressInfo;
            address.IsInvoiceAddress = model.IsInvoiceAddress;
            address.InvoiceAddressName = model.InvoiceAddressName;
            address.InvoiceFirstName = model.InvoiceFirstName;
            address.InvoiceLastName = model.InvoiceLastName;
            address.InvoicePhoneNumber = model.InvoicePhoneNumber;
            address.InvoiceEmail = model.InvoiceEmail;
            address.InvoiceCity = model.InvoiceCity;
            address.InvoiceDistrict = model.InvoiceDistrict;
            address.InvoiceNeighborhood = model.InvoiceNeighborhood;
            address.InvoiceAddressInfo = model.InvoiceAddressInfo;
            address.TCNumber = model.TCNumber;
            address.PassaportNumber = model.PassaportNumber;
            address.PassaportCountry = model.PassaportCountry;
            address.CommercialTitle = model.CommercialTitle;
            address.TaxAdministration = model.TaxAdministration;
            address.TaxNumber = model.TaxNumber;
            address.InvoiceType = model.InvoiceType;
            address.InvoicePayer = model.InvoicePayer;

            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Address updated successfully." });
        }

    }
}
