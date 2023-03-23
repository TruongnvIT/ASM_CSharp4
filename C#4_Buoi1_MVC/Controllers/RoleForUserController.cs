using C_4_Buoi1_MVC.Repositories.IService;
using C_4_Buoi1_MVC.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace C_4_Buoi1_MVC.Controllers
{
    public class RoleForUserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public RoleForUserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> ManagerRoleUser()
        {
            var users = _userManager.Users.ToList();
            var UserRoleAspNetVMs = new List<UserRoleAspNetVM>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserRoleAspNetVM userRoleAspNetVM = new UserRoleAspNetVM()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles
                };
                UserRoleAspNetVMs.Add(userRoleAspNetVM);
            }
            return View(UserRoleAspNetVMs);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateRoleUserView(string id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = await _userManager.GetRolesAsync(user);

            UserRoleAspNetVM userRoleAspNetVM = new UserRoleAspNetVM()
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles
            };
            return View(userRoleAspNetVM);
        }

        public async Task<IActionResult> UpdateRoleUser(string id, string role)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (role == null)
                {
                    foreach (var item in roles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, item);
                    }
                    await _userManager.AddToRoleAsync(user, "USER");
                }
                else if (!roles.Any(c => c == role))
                {
                    foreach (var item in roles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, item);
                    }
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            return RedirectToAction("ManagerRoleUser");
        }

        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
        //    if (user != null)
        //    {
        //        var roles = await _userManager.GetRolesAsync(user);
        //        if (roles != null)
        //        {
        //            await _userManager.RemoveFromRolesAsync(user, roles);
        //        }
        //        await _userManager.DeleteAsync(user);
        //        _userService.Delete(Guid.Parse(id));
        //    }
        //    return RedirectToAction("ManagerRoleUser");
        //}
    }
}
