using C_4_Buoi1_MVC.Data.CShap4DbContext;
using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;
using Microsoft.EntityFrameworkCore;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public class CartDetailService : ICartDetailService
    {
        private readonly CSharp4DbContext _context;
        public CartDetailService()
        {
            _context = new();
        }
        public bool Create(CartDetails cartDetails)
        {
            try
            {
                _context.CartDetails.Add(cartDetails);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var resutl = _context.CartDetails.FirstOrDefault(c => c.Id == id);
                _context.CartDetails.Remove(resutl);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public List<CartDetails> GetAll()
        {
            return _context.CartDetails.ToList();
        }

        public bool Update(CartDetails cartDetails)
        {
            try
            {
                _context.CartDetails.Update(cartDetails);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
