using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories;
using C_4_Buoi1_MVC.Repositories.IService;
using C_4_Buoi1_MVC.ViewModel;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace C_4_Buoi1_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _iProduct;
        private readonly IUserService _iUser;
        private readonly IRoleService _iRole;

        public ProductController(IProductService iProduct, IUserService iUser, IRoleService iRole)
        {
            _iProduct = iProduct;
            _iUser = iUser;
            _iRole = iRole;
        }

        public IActionResult GetListView()
        {            
            return View(_iProduct.GetList().Where(c=>c.AvailbleQuantity > 0 && c.Status == 0));
        }
        public IActionResult Manager()
        {
            return View(_iProduct.GetList());
        }

        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = productVM.Name,
                    Price = productVM.Price,
                    Status = productVM.Status,
                    Supplier = productVM.Supplier,
                    Description = productVM.Description,
                    Image = await _iProduct.ConverImageToByteArr(productVM.Image)
                };
                var resutl = _iProduct.Create(product);
                if (resutl)
                {

                    //ViewBag.Message = "Thêm mới thành công";
                    return RedirectToAction("Manager");
                }
                //ViewBag.Message = "Thêm mới thất bại";
                return View("Create");
            }
            return View("Create");
        }

        public IActionResult Delete(Guid id)
        {
            _iProduct.Delete(id);
            
            return RedirectToAction("Manager");
        }

        public IActionResult Update(Guid id)
        {
            return View(_iProduct.GetById(id));
        }

        public async Task<IActionResult> UpdateSp(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Id = productVM.Id,
                    Name = productVM.Name,
                    Price = productVM.Price,
                    Status = productVM.Status,
                    Supplier = productVM.Supplier,
                    Description = productVM.Description,
                    AvailbleQuantity = productVM.AvailbleQuantity,
                    Image = await _iProduct.ConverImageToByteArr(productVM.Image)
                };
                var resutl = _iProduct.Update(product);
                if (resutl)
                {
                    return RedirectToAction("Manager");
                }

                return View("Update", _iProduct.GetById(productVM.Id));
            }
            return View("Update", _iProduct.GetById(productVM.Id));
        }

    }
}
