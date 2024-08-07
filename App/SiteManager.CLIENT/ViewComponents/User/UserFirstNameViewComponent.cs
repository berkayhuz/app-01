using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Services.Abstractions;
using System.Threading.Tasks;

namespace SiteManager.CLIENT.ViewComponents.User
{
    [Authorize]
    public class UserFirstNameViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public UserFirstNameViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserAsync();
            if (user != null)
            {
                return View("/Views/Shared/Components/User/UserInformation/FirstName.cshtml", user.FirstName);
            }

            return Content("Username not found");
        }
    }
}