using C_4_Buoi1_MVC.Repositories.IService;
using Microsoft.AspNetCore.Mvc;

namespace C_4_Buoi1_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _iUser;
        public UserController(IUserService iUser)
        {
            _iUser = iUser;
           
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetString("iduser", "");
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var users = _iUser.GetList();
            var user = users.FirstOrDefault(c => c.UserName == username && c.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("iduser", user.Id.ToString());
                return RedirectToAction("GetListView", "Product");
            }
            ViewBag.ToastMessage = "Thông báo đã được hiển thị!";
            return RedirectToAction("Index");
        }
    }
}
