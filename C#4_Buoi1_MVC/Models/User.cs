namespace C_4_Buoi1_MVC.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? IdRole { get; set; }
        public int Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<Bill> Bill { get; set; }

    }
}
