namespace C_4_Buoi1_MVC.ViewModel
{
    public class UserRoleAspNetVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
