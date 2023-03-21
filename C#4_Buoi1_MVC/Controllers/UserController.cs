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
            return View();
        }
    }
}
