using C_4_Buoi1_MVC.Models;

namespace C_4_Buoi1_MVC.Repositories.IService
{
    public interface IBillService
    {
        public bool Create(Bill bill);
        public bool Update(Bill bill);
        public bool Delete(Guid id);
        public List<Bill> GetAll();
    }
}
