using System.ComponentModel.DataAnnotations;

namespace C_4_Buoi1_MVC.ViewModel
{
    public class ProductVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Tên sản phẩm")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Giá sản phẩm")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Số lượng")]
        public int AvailbleQuantity { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Nhà cung cấp")]
        public string Supplier { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Ảnh sản phẩm")]
        public IFormFile Image { get; set; }
        public string? Description { get; set; }
    }
}
