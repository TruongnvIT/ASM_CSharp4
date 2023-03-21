namespace C_4_Buoi1_MVC.Models
{
    public class BillDetails
    {
        public Guid Id { get; set; }
        public Guid IdSP { get; set; }
        public Guid IdHD { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }


        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
