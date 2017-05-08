using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Repository
{
    public interface ICategoryRepository
    {
        CategoryModel[] Categories { get; }
        CategoryModel Category(int categoryId);
    }

    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryRepository : ICategoryRepository
    {
        public CategoryModel[] Categories
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                                               .Select(t => new CategoryModel { Id = t.ClassId, Name = t.ClassName })
                                               .ToArray();
            }
        }

        public CategoryModel Category(int categoryId)
        {
            var category = DatabaseAccessor.Instance.Classes
                                                   .Where(t => t.ClassId == categoryId)
                                                   .Select(t => new CategoryModel { Id = t.ClassId, Name = t.ClassName })
                                                   .First();
            return category;
        }
    }
}
