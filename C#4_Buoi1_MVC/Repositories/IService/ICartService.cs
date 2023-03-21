using C_4_Buoi1_MVC.Models;

namespace C_4_Buoi1_MVC.Repositories.IService
{
    public interface ICartService
    {
        public bool Create(Cart cart);
        public bool Update(Cart cart);
        public bool Delete(Guid id);
        public List<Cart> GetAll();
    }
}
