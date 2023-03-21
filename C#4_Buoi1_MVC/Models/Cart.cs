namespace C_4_Buoi1_MVC.Models
{
    public class Cart
    {
        public Guid IdUser { get; set; }
        public string? Description { get; set; }   

        public virtual User User { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }
}
