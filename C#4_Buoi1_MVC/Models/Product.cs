namespace C_4_Buoi1_MVC.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int AvailbleQuantity { get; set; }
        public int Status { get; set; }
        public string Supplier { get; set; }
        public byte[] Image { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<BillDetails> BillDetails { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; }
}
}
