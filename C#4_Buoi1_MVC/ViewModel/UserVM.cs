using System.ComponentModel.DataAnnotations;

namespace C_4_Buoi1_MVC.ViewModel
{
    public class UserVM
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email không đúng định dạng")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Password")]
        [StringLength(20, MinimumLength = 8, ErrorMessage ="Mật khẩu phải từ 8 - 20 kí tự")]
        public string Password { get; set; }
        public Guid? IdRole { get; set; }
        public int Status { get; set; }
    }
}
