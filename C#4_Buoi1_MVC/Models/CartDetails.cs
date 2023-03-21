namespace C_4_Buoi1_MVC.Models
{
    public class CartDetails
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdSP { get; set; }
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
