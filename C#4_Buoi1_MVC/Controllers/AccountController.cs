using C_4_Buoi1_MVC.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace C_4_Buoi1_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        public IActionResult GoogleSignIn()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback"),
                Items =
                        {
                            { "Scheme", "Google" }
                        }
            };

            return Challenge(properties, "Google");
        }
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync("Google");

            if (result.Succeeded)
            {
                // Lấy thông tin người dùng từ Google
                var accessToken = result.Properties.GetTokens().FirstOrDefault(token => token.Name == "access_token")?.Value;
                var userinfo = await GetGoogleUserInfoAsync(accessToken);
            }
            return RedirectToAction("GetListView", "Product");
        }

        private async Task<GoogleUserInfoVM> GetGoogleUserInfoAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var userinfo = JsonConvert.DeserializeObject<GoogleUserInfoVM>(json);

            return userinfo;
        }

    }
}
