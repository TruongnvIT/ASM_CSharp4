using C_4_Buoi1_MVC.Data.CShap4DbContext;
using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public class RoleService : IRoleService
    {
        private readonly CSharp4DbContext _context;
        public RoleService()
        {
            _context = new();
        }

        public bool Create(Role cart)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Role GetById(Guid? id)
        {
            var role = _context.Roles.ToList().FirstOrDefault(c => c.Id == id);
            if (role != null)
            {
                return _context.Roles.ToList().FirstOrDefault(c => c.Id == id);
            }
            return new Role();
        }
        public bool Update(Role cart)
        {
            throw new NotImplementedException();
        }
    }
}
