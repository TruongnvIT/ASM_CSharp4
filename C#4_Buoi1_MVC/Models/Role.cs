namespace C_4_Buoi1_MVC.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
