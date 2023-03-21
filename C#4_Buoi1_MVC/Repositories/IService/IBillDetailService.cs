using C_4_Buoi1_MVC.Models;

namespace C_4_Buoi1_MVC.Repositories.IService
{
    public interface IBillDetailService
    {
        public bool Create(BillDetails billDetail);
        public bool Update(BillDetails billDetail);
        public bool Delete(Guid id);
        public List<BillDetails> GetAll();
    }
}
