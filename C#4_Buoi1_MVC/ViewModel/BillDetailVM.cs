using C_4_Buoi1_MVC.Models;

namespace C_4_Buoi1_MVC.ViewModel
{
    public class BillDetailVM
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Bill Bill { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
