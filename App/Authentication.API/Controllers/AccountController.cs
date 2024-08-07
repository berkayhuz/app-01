using Authentication.API.Data;
using Authentication.API.Helpers;
using Authentication.API.Helpers.TokenHelpers;
using Authentication.LIB.Entities;
using Authentication.LIB.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
            _configuration = configuration;
        }

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!model.ExpressConsentText || !model.KvkkPolicy)
			{
				return BadRequest(new { Message = "Express Consent Text and KVKK Policy must be accepted." });
			}

			var user = new AppUser(model.Email, model.ExpressConsentText, model.KvkkPolicy);
			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await SendConfirmationEmail(user);
				return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
			}

			AddErrorsToModelState(result.Errors);
			return BadRequest(ModelState);
		}

		[HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                Log.Warning("Invalid email confirmation request for {Email}", email);
                return BadRequest("Invalid Email Confirmation Request");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                Log.Information("Email confirmed for {Email}", email);
                return Redirect("https://localhost:7239/account/confirmedsuccess");
            }

            Log.Warning("Email confirmation failed for {Email}", email);
            return BadRequest("Email confirmation failed");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                Log.Warning("Login attempt failed for non-existent email {Email}", model.Email);
                return Unauthorized(new { Message = "Böyle bir hesap bulunamadı." });
            }

            if (!user.EmailConfirmed)
            {
                await SendConfirmationEmail(user);
                Log.Warning("Email not confirmed for {Email}. Confirmation email sent.", model.Email);
                return Unauthorized(new { Message = "Email not confirmed yet. A new confirmation link has been sent to your email." });
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var tokenService = new TokenService(_configuration);
                var token = tokenService.GenerateToken(user);

                var tokenStorageService = HttpContext.RequestServices.GetService<ITokenStorageService>();
                await tokenStorageService.StoreTokenAsync(user.Id, token);

                Log.Information("User {Email} logged in successfully", model.Email);
                return Ok(new { Token = token });
            }
            else if (result.IsLockedOut)
            {
                Log.Warning("User {Email} account locked out", model.Email);
                return Unauthorized(new { Message = "Account locked out due to multiple failed login attempts. Please try again later." });
            }

            Log.Warning("Login attempt failed for {Email}. Invalid password or other login failure.", model.Email);
            return Unauthorized(new { Message = "Invalid login attempt." });
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found");

            await SendPasswordResetEmail(user);
            return Ok(new { Message = "Password reset email sent. Please check your email." });
        }
        [HttpGet("resetpassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            var redirectUrl = Url.Action("ResetPassword", "Account", new { token, email });
            var fullRedirectUrl = $"{Request.Scheme}://localhost:7239{redirectUrl.Replace("/api", string.Empty)}";
            return Redirect(fullRedirectUrl);
        }
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found");

            if (await IsTokenUsed(model.Token))
                return BadRequest("This password reset token has already been used.");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                await MarkTokenAsUsed(model.Token);
                return Ok("Password has been reset successfully");
            }

            AddErrorsToModelState(result.Errors);
            return BadRequest(ModelState);
        }
        [HttpGet("getuser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUser()
        {
            var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(loggedInUserEmail))
                return Unauthorized("User is not logged in.");

            var user = await _userManager.FindByEmailAsync(loggedInUserEmail);
            if (user == null)
                return NotFound("User not found");

            var model = new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return Ok(model);
        }
        [HttpPost("changeemail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangeEmail(ChangeEmailModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(loggedInUserEmail))
                return Unauthorized("User is not logged in.");

            var user = await _userManager.FindByEmailAsync(loggedInUserEmail);
            if (user == null)
                return NotFound("User not found");

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
            var changeEmailLink = Url.Action(nameof(ConfirmChangeEmail), "Account", new { userId = user.Id, token, newEmail = model.NewEmail }, Request.Scheme);
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "change_email_template.html");

            try
            {
                await _emailSender.SendEmailAsync(user.Email, "Confirm your email change", changeEmailLink, user.UserName, templatePath);
                Log.Information("Change email confirmation sent to {Email}", user.Email);
                return Ok(new { Message = "Change email confirmation link has been sent to your current email address. Please check your email." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error sending change email confirmation to {Email}", user.Email);
                throw;
            }
        }
        [HttpGet("confirmchangeemail")]
        public async Task<IActionResult> ConfirmChangeEmail(string userId, string token, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            if (result.Succeeded)
            {
                user.Email = newEmail;
                user.NormalizedEmail = _userManager.NormalizeEmail(newEmail);
                user.UserName = newEmail;
                user.NormalizedUserName = _userManager.NormalizeName(newEmail);

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                    Log.Information("Email changed for {Email} to {NewEmail}", user.Email, newEmail);
                    return Ok(new { Message = "Email address has been changed successfully." });
                }
                else
                {
                    Log.Warning("Email changed but updating user information failed for {Email}", user.Email);
                    return BadRequest("Email address has been changed, but updating user information failed.");
                }
            }

            Log.Warning("Email change failed for {Email}", user.Email);
            return BadRequest("Email change failed");
        }

		private async Task SendConfirmationEmail(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "email_template.html");

            try
            {
                await _emailSender.SendEmailAsync(user.Email, "Confirm your email", confirmationLink, user.UserName, templatePath);
                Log.Information("Confirmation email sent to {Email}", user.Email);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error sending confirmation email to {Email}", user.Email);
                throw;
            }
        }
        private async Task SendPasswordResetEmail(AppUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "forgot_password_template.html");

            try
            {
                await _emailSender.SendEmailAsync(user.Email, "Reset your password", resetLink, user.UserName, templatePath);
                Log.Information("Password reset email sent to {Email}", user.Email);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error sending password reset email to {Email}", user.Email);
                throw;
            }
        }
        private async Task<bool> IsTokenUsed(string token)
        {
            var tokenHash = ComputeSha256Hash(token);
            return await _context.UsedTokens.AnyAsync(t => t.Token == tokenHash);
        }
        private async Task MarkTokenAsUsed(string token)
        {
            var tokenHash = ComputeSha256Hash(token);
            _context.UsedTokens.Add(new UsedToken
            {
                Token = tokenHash,
                UsedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
        private void AddErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private string ComputeSha256Hash(string rawData)
        {
            using (var sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
