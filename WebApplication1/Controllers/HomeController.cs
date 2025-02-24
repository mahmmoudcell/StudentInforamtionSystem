using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the logged-in user's details
                var user = await _userManager.GetUserAsync(User);
                var viewModel = new HomeViewModel
                {
                    UserName = user?.UserName,
                    Email = user?.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };

                return View(viewModel);
            }

            // For unauthenticated users, show a default homepage
            return View("Default");
        }
    }

    // ViewModel for passing user data to the view
    public class HomeViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
