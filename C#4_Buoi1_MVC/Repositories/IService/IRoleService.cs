using C_4_Buoi1_MVC.Models;

namespace C_4_Buoi1_MVC.Repositories.IService
{
    public interface IRoleService
    {
        public bool Create(Role cart);
        public bool Update(Role cart);
        public bool Delete(Guid id);
        public List<Role> GetAll();
        public Role GetById(Guid? id);
    }
}
