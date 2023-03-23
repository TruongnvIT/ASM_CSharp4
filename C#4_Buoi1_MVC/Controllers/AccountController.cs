using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;
using C_4_Buoi1_MVC.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using C_4_Buoi1_MVC.Repositories.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace C_4_Buoi1_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        private readonly IUserService _iUser;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger, IUserService userService, IUserService iUser)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _userService = userService;
            _iUser = iUser;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            //HttpContext.Session.SetString("iduser", "");
            return View();
        }


        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{
        //    var users = _iUser.GetList();
        //    var user = users.FirstOrDefault(c => c.UserName == username && c.Password == password);
        //    if (user != null)
        //    {
        //        HttpContext.Session.SetString("iduser", user.Id.ToString());
        //        return RedirectToAction("GetListView", "Product");
        //    }
        //    ViewBag.ToastMessage = "Thông báo đã được hiển thị!";
        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
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
                if (await _userManager.FindByEmailAsync(userinfo.Email) == null && userinfo.HostedDomain == "fpt.edu.vn")
                {
                    var iduser = Guid.NewGuid();
                    IdentityUser identityUser = new IdentityUser()
                    {
                        Id = iduser.ToString(),
                        Email = userinfo.Email,
                        UserName = userinfo.Email,
                    };
                    var adduser = await _userManager.CreateAsync(identityUser);

                    var addroleforuser = await _userManager.AddToRoleAsync(identityUser, "USER");
                    if (adduser.Succeeded)
                    {
                        User user = new User()
                        {
                            Id = iduser,
                            UserName = userinfo.Email,
                            Password = userinfo.Email,
                            Status = 0
                        };
                        _userService.Create(user);
                    }
                }
                var userIdentity = await _userManager.FindByEmailAsync(userinfo.Email);
                var roles = await _userManager.GetRolesAsync(userIdentity);
                var claims = new List<Claim>();
                
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));                
                
                if (await _userManager.FindByEmailAsync(userinfo.Email) != null)
                {
                    HttpContext.Session.SetObject("UserLogin", userinfo); // lưu đối tượng vào session với key là "User"
                    var user = _iUser.GetList().FirstOrDefault(c => c.UserName == userinfo.Email);
                    HttpContext.Session.SetObject("user", user);
                    return RedirectToAction("GetListView", "Product");
                }
            }
            return RedirectToAction("Login", "Account");
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
