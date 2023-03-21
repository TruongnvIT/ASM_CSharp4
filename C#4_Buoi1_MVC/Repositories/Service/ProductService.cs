using C_4_Buoi1_MVC.Data.CShap4DbContext;
using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;
using C_4_Buoi1_MVC.ViewModel;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public class ProductService : IProductService
    {
        public readonly CSharp4DbContext _context;
        public ProductService()
        {
            _context = new();
        }

        public async Task<byte[]> ConverImageToByteArr(IFormFile formFile)
        {
            var ms = new MemoryStream();
            await formFile.CopyToAsync(ms);
            return ms.ToArray();
        }

        public IFormFile ConverImageToIFormFile(byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            return new FormFile(memoryStream, 0, bytes.Length, "fileName", "fileName")
            {
                Headers = new HeaderDictionary(),
                ContentType = "Nội dung của tệp"
            };
        }
        public List<Product> GetList()
        {
            return _context.Products.Where(c=>c.Status != 1).ToList();
        }

        public ProductVM GetById(Guid id)
        {
            try
            {
                var product = _context.Products.ToList().FirstOrDefault(c => c.Id == id && c.Status != 1);

                return new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Status = product.Status,
                    Description = product.Description,
                    AvailbleQuantity = product.AvailbleQuantity,
                    Supplier = product.Supplier,
                    Image = ConverImageToIFormFile(product.Image)
                };
            }
            catch (Exception)
            {
                return new ProductVM();

            }

        }
        public bool Create(Product Product)
        {
            if (Product != null)
            {
                _context.Products.Add(Product);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Product Product)
        {
            try
            {
                var result = _context.Products.ToList().FirstOrDefault(c => c.Id == Product.Id);
                if (result != null)
                {
                    result.Name = Product.Name;
                    result.Status = Product.Status;
                    result.Price = Product.Price;
                    result.AvailbleQuantity = Product.AvailbleQuantity;
                    result.Supplier = Product.Supplier;
                    result.Description = Product.Description;
                    result.Image = Product.Image;

                    _context.Products.Update(result);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(Guid id)
        {
            try
            {
                var result = _context.Products.ToList().FirstOrDefault(c => c.Id == id);
                if (result != null)
                {
                    result.Status = 1;
                    _context.Products.Update(result);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
