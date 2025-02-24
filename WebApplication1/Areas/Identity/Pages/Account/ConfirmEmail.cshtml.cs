using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ConfirmEmailModel> _logger;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, ILogger<ConfirmEmailModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                _logger.LogWarning("Invalid confirmation link: Missing userId or code.");
                StatusMessage = "Invalid confirmation link.";
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError($"User not found: ID {userId}");
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);

                StatusMessage = result.Succeeded
                    ? "Thank you for confirming your email."
                    : "Error confirming your email.";
                _logger.LogInformation($"Email confirmation result for user {userId}: {result.Succeeded}");

                if (!result.Succeeded)
                {
                    _logger.LogWarning($"Email confirmation failed for user {userId}: {string.Join(", ", result.Errors)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error confirming email for user {userId}.");
                StatusMessage = "An error occurred while confirming your email.";
            }

            return Page();
        }
    }
}
