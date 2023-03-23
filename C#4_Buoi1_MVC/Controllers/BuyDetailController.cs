using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories;
using C_4_Buoi1_MVC.Repositories.IService;
using C_4_Buoi1_MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C_4_Buoi1_MVC.Controllers
{
    public class BuyDetailController : Controller
    {
        private readonly IProductService _iProduct;
        private readonly IBillService _ibill;
        private readonly IBillDetailService _ibillDetail;
        public BuyDetailController(IProductService iProduct, IBillDetailService ibillDetail, IBillService ibill)
        {
            _iProduct = iProduct;
            _ibill = ibill;
            _ibillDetail = ibillDetail;
        }
        public IActionResult Detail(Guid id)
        {
            return View(_iProduct.GetList().FirstOrDefault(c => c.Id == id));
        }

        public IActionResult GetAll(string iduser)
        {
            return View(_ibill.GetAll().Where(c => c.UserId == Guid.Parse(iduser)).OrderByDescending(c => c.CreateDate));
        }

        public IActionResult GetById(Guid id)
        {
            return View(_ibillDetail.GetAll().Where(c => c.IdHD == id));
        }

        public IActionResult Buy(Guid idSP, int quantity, string iduser)
        {
            var newidbill = Guid.NewGuid();
            var product = _iProduct.GetList().FirstOrDefault(c => c.Id == idSP);

            Bill bill = new Bill
            {
                Id = newidbill,
                CreateDate = DateTime.Now,
                UserId = Guid.Parse(iduser),
                Status = 0
            };
            var resutl = _ibill.Create(bill);
            if (resutl)
            {
                BillDetails billDetails = new BillDetails
                {
                    Id = Guid.NewGuid(),
                    IdSP = idSP,
                    IdHD = newidbill,
                    Price = product != null ? product.Price : 0,
                    Quantity = quantity
                };
                var resutldt = _ibillDetail.Create(billDetails);
                bool resutlpro = false;
                if (resutldt)
                {
                    product.AvailbleQuantity = product.AvailbleQuantity - quantity;
                    resutlpro = _iProduct.Update(product);
                }
                if (resutldt && resutlpro)
                {
                    return View("DetailBuyed", _ibillDetail.GetAll().Where(c => c.IdHD == newidbill));
                }
                return View(_iProduct.GetList().FirstOrDefault(c => c.Id == idSP));
            }
            return View(_iProduct.GetList().FirstOrDefault(c => c.Id == idSP));
        }

        public IActionResult BuyList(string jsonList)
        {
            var cartDetails = JsonConvert.DeserializeObject<List<CartDetails>>(jsonList);
            var newidbill = Guid.NewGuid();
            var iduser = cartDetails.FirstOrDefault().IdUser;
            try
            {
                Bill bill = new Bill
                {
                    Id = newidbill,
                    CreateDate = DateTime.Now,
                    UserId = iduser,
                    Status = 0
                };
                var resutl = _ibill.Create(bill);
                if (resutl)
                {
                    foreach (var item in cartDetails)
                    {
                        var product = _iProduct.GetList().FirstOrDefault(c => c.Id == item.IdSP);
                        BillDetails billDetails = new BillDetails
                        {
                            Id = Guid.NewGuid(),
                            IdSP = item.IdSP,
                            IdHD = newidbill,
                            Price = product.Price,
                            Quantity = item.Quantity
                        };
                        var resutldt = _ibillDetail.Create(billDetails);
                        if (resutldt)
                        {
                            product.AvailbleQuantity = product.AvailbleQuantity - item.Quantity;
                            _iProduct.Update(product);
                        }
                    }
                    return View("DetailBuyed", _ibillDetail.GetAll().Where(c => c.IdHD == newidbill));
                }
                return RedirectToAction("GetCartDetails", "AddCart", iduser);
            }
            catch (Exception)
            {
                return RedirectToAction("GetCartDetails", "AddCart", iduser);
            }
        }

        public IActionResult DetailBuyed(Guid idbill)
        {
            return View(_ibillDetail.GetAll().Where(c => c.IdHD == idbill));
        }

    }
}
