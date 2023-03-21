using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories;
using C_4_Buoi1_MVC.Repositories.IService;
using Microsoft.AspNetCore.Mvc;

namespace C_4_Buoi1_MVC.Controllers
{
    public class AddCartController : Controller
    {
        private readonly IProductService _iProduct;
        private readonly ICartService _iCart;
        private readonly ICartDetailService _iCartDetail;
        public AddCartController(IProductService iProduct, ICartDetailService iCartDetail, ICartService iCart)
        {
            _iProduct = iProduct;
            _iCart = iCart;
            _iCartDetail = iCartDetail;
        }
        public IActionResult AddCart(Guid idSP, int quantity, string iduser)
        {

            if (!_iCart.GetAll().Any(c => c.IdUser == Guid.Parse(iduser)))
            {
                Cart cart = new Cart()
                {
                    IdUser = Guid.Parse(iduser)
                };
                var resutl = _iCart.Create(cart);
            }

            bool resutldt;
            if (!_iCartDetail.GetAll().Any(c => c.IdSP == idSP && c.IdUser == Guid.Parse(iduser)))
            {
                CartDetails cartDetails = new CartDetails()
                {
                    Id = Guid.NewGuid(),
                    IdSP = idSP,
                    Quantity = quantity,
                    IdUser = Guid.Parse(iduser)
                };
                resutldt = _iCartDetail.Create(cartDetails);
            }
            else
            {
                var cartdetail = _iCartDetail.GetAll().FirstOrDefault(c => c.IdSP == idSP && c.IdUser == Guid.Parse(iduser));
                cartdetail.Quantity = cartdetail.Quantity + quantity;
                resutldt = _iCartDetail.Update(cartdetail);
            }

            if (resutldt)
            {
                return RedirectToAction("GetListView", "Product");
            }
            return RedirectToAction($"BuyDetail/Detail/{idSP}&{quantity}&{iduser}");
        }

        public IActionResult GetCartDetails(string iduser)
        {
            return View(_iCartDetail.GetAll().Where(c => c.IdUser == Guid.Parse(iduser)));
        }
    }
}
