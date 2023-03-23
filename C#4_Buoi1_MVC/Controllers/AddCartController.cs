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
        public IActionResult AddCart(Guid idSP, int quantity, Guid iduser)
        {
            var product = _iProduct.GetList().FirstOrDefault(c => c.Id == idSP);

            if (!_iCart.GetAll().Any(c => c.IdUser == iduser))
            {
                Cart cart = new Cart()
                {
                    IdUser = iduser
                };
                var resutl = _iCart.Create(cart);
            }

            bool resutldt;
            if (!_iCartDetail.GetAll().Any(c => c.IdSP == idSP && c.IdUser == iduser))
            {
                CartDetails cartDetails = new CartDetails()
                {
                    Id = Guid.NewGuid(),
                    IdSP = idSP,
                    Quantity = quantity,
                    IdUser = iduser
                };
                resutldt = _iCartDetail.Create(cartDetails);
            }
            else
            {
                var cartdetail = _iCartDetail.GetAll().FirstOrDefault(c => c.IdSP == idSP && c.IdUser == iduser);
                cartdetail.Quantity = cartdetail.Quantity + quantity;
                resutldt = _iCartDetail.Update(cartdetail);
            }
            product.AvailbleQuantity = product.AvailbleQuantity - quantity;
            var resutlpro = _iProduct.Update(product);
            if (resutldt && resutlpro)
            { 
                return RedirectToAction("GetListView", "Product");
            }
            return RedirectToAction($"BuyDetail/Detail/{idSP}&{quantity}&{iduser}");
        }

        public IActionResult GetCartDetails(Guid iduser)
        {
            return View(_iCartDetail.GetAll().Where(c => c.IdUser == iduser));
        }

        public IActionResult RemoveCartDetail(Guid id, Guid iduser)
        {
            _iCartDetail.Delete(id);

            return RedirectToAction("GetCartDetails", iduser);

        }
    }
}
