using C_4_Buoi1_MVC.Models;

namespace C_4_Buoi1_MVC.Repositories.IService
{
    public interface ICartDetailService
    {
        public bool Create(CartDetails cartDetails);
        public bool Update(CartDetails cartDetails);
        public bool Delete(Guid id);
        public List<CartDetails> GetAll();
    }
}
