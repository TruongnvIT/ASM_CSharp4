using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace C_4_Buoi1_MVC.Repositories.IService
{
    public interface IProductService
    {
        public Task<byte[]> ConverImageToByteArr(IFormFile formFile);
        public IFormFile ConverImageToIFormFile(byte[] bytes);
        public List<Product> GetList();
        public ProductVM GetById(Guid id);
        public bool Create(Product Product);
        public bool Update(Product Product);
        public bool Delete(Guid id);

    }
}
