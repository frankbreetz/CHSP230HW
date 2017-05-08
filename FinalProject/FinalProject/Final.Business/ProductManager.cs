using System.Linq;
using Final.Repository;

namespace Final.Business
{
    public interface IProductManager
    {
        ProductModel[] ForCategory(int categoryId);
    }

    public class ProductModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
    }

    public class ProductManager : IProductManager
    {
        private readonly IProductRepository productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductModel[] ForCategory(int categoryId)
        {
            return productRepository.ForCategory(categoryId).Select(t =>
                            new ProductModel
                            {
                                Id = t.Id,
                                Email = t.Email,
                                IsAdmin = t.IsAdmin,
                                Password = t.Password
                            })
                            .ToArray();
        }
    }
}
