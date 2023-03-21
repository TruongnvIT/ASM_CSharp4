using C_4_Buoi1_MVC.Data.CShap4DbContext;
using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;
using Microsoft.EntityFrameworkCore;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public class BillService : IBillService
    {
        private readonly CSharp4DbContext _context;
        public BillService()
        {
            _context = new();
        }
        public bool Create(Bill bill)
        {
            try
            {
                _context.Bills.Add(bill);
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

        public bool Update(Bill bill)
        {
            throw new NotImplementedException();
        }

        public List<Bill> GetAll()
        {
            return _context.Bills.ToList();
        }
    }
}
