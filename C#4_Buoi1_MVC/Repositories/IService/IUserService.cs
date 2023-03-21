using C_4_Buoi1_MVC.Models;

namespace C_4_Buoi1_MVC.Repositories.IService
{
    public interface IUserService
    {
        public bool Create(User cart);
        public bool Update(User cart);
        public bool Delete(Guid id);
        public List<User> GetList();
        public User GetById(Guid? id);
    }
}
