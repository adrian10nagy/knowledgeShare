using DAL.Entities;
using Softvision.BL.Entities;
using Softvision.BL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Crud
{
    public class CategoryCrud
    {
        Category article = new Category();

        public List<CategoryBL> GetAll()
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Category> categories = DAL.SDK.Kit.Instance.Categories.GetAll();
            List<CategoryBL> mappedCategories = poMapper.MapCategoryCollection(categories).ToList();
            return mappedCategories;
        }

        public void Insert(string categoryName)
        {
            DAL.SDK.Kit.Instance.Categories.Insert(categoryName);
        }

    }
}
