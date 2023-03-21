using C_4_Buoi1_MVC.Data.CShap4DbContext;
using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;
using Microsoft.EntityFrameworkCore;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public class CartService : ICartService
    {
        private readonly CSharp4DbContext _context;
        public CartService()
        {
            _context = new();
        }
        public bool Create(Cart cart)
        {
            try
            {
                _context.Carts.Add(cart);
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
            throw new NotImplementedException();
        }
        public bool Update(Cart cart)
        {
            throw new NotImplementedException();
        }

        public List<Cart> GetAll()
        {
           return _context.Carts.ToList();
        }
    }
}
