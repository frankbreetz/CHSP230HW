using System.Linq;

namespace Final.Repository
{
    public interface IProductRepository
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

    public class ProductRepository : IProductRepository
    {
        public ProductModel[] ForCategory(int categoryId)
        {
            return DatabaseAccessor.Instance.Classes.First(t => t.ClassId == categoryId)
                                  .Users.Select(t =>
                                        new ProductModel
                                        {
                                            Id = t.UserId,
                                            Email = t.UserEmail,
                                            IsAdmin = t.UserIsAdmin,
                                            Password = t.UserPassword
                                        })
            .ToArray();
        }
    }
}