using C_4_Buoi1_MVC.Data.CShap4DbContext;
using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public class UserService : IUserService
    {
        public readonly CSharp4DbContext _context;
        public UserService()
        {
            _context = new();
        }

        public List<User> GetList()
        {
            return _context.Users.Where(c => c.Status != 1).ToList();
        }

        public bool Create(User user)
        {
            if (user != null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(User user)
        {
            try
            {
                var result = _context.Users.ToList().FirstOrDefault(c => c.Id == user.Id);
                if (result != null)
                {
                    result.UserName = user.UserName;
                    result.Status = user.Status;
                    result.Password = user.Password;
                    result.IdRole = user.IdRole;

                    _context.Users.Update(result);
                    _context.SaveChanges();
                    return true;
                }

                return false;
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
                var result = _context.Users.ToList().FirstOrDefault(c => c.Id == id);
                if (result != null)
                {
                    _context.Users.Remove(result);
                    _context.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public User GetById(Guid? id)
        {
            throw new NotImplementedException();
        }
    }
}
