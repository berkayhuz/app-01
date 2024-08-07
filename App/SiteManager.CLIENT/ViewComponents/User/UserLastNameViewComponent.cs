using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Services.Abstractions;

namespace SiteManager.CLIENT.ViewComponents.User
{
	[Authorize]
	public class UserLastNameViewComponent : ViewComponent
	{
		private readonly IUserService _userService;

		public UserLastNameViewComponent(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await _userService.GetUserAsync();
			if (user != null)
			{
				return View("/Views/Shared/Components/User/UserInformation/LastName.cshtml", user.LastName);
			}

			return Content("Username not found");
		}
	}
}