using C_4_Buoi1_MVC.Data.CShap4DbContext;
using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;
using C_4_Buoi1_MVC.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public class BillDetailService : IBillDetailService
    {
        private readonly CSharp4DbContext _context;
        public BillDetailService()
        {
            _context = new();
        }
        public bool Create(BillDetails billDetail)
        {
            try
            {
                _context.BillDetails.Add(billDetail);
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

        public bool Update(BillDetails billDetail)
        {
            throw new NotImplementedException();
        }

        public List<BillDetails> GetAll()
        {
            return _context.BillDetails.ToList();
        }
    }
}
