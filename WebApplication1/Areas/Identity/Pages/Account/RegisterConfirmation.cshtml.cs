using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WebApplication1.Models;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _sender;
        private readonly ILogger<RegisterConfirmationModel> _logger;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender sender, ILogger<RegisterConfirmationModel> logger)
        {
            _userManager = userManager;
            _sender = sender;
            _logger = logger;
        }

        public string Email { get; set; }
        public bool DisplayConfirmAccountLink { get; set; }
        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogWarning("No email provided for confirmation.");
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogError($"Unable to load user with email '{email}'.");
                return NotFound($"Unable to load user with email '{email}'.");
            }

            // Generate email confirmation token and encode it
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            EmailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

            try
            {
                // Send confirmation email
                await _sender.SendEmailAsync(
                    email,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(EmailConfirmationUrl)}'>clicking here</a>.");
                _logger.LogInformation($"Confirmation email sent to {email}.");

                // Optionally, set the user's email as confirmed (this can be handled after user clicks the link)
                // This line might not be needed if you're waiting for the user to click the link to confirm
                // user.EmailConfirmed = true;
                // await _userManager.UpdateAsync(user);

                _logger.LogInformation($"Email confirmation for user with email '{email}' is set to true after confirmation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email to {email}.");
                // Optionally handle errors (e.g., show a message to the user)
                return Page();
            }

            Email = email;
            return Page();
        }
    }
}
