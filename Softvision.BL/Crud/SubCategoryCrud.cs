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
    public class SubCategoryCrud
    {
        SubCategory subCategory = new SubCategory();

        public List<SubCategoryBL> GetAll()
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<SubCategory> subCategories = DAL.SDK.Kit.Instance.SubCategories.GetAll();
            List<SubCategoryBL> mappedSubCategories = poMapper.MapSubCategoryCollection(subCategories).ToList();
            return mappedSubCategories;
        }

        public void Insert(int categoryId, string subCategoryName)
        {
            DAL.SDK.Kit.Instance.SubCategories.Insert(categoryId, subCategoryName);
        }

        public  List<SubCategoryBL> GetAllByCategoryId(int categoryId)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<SubCategory> subCategories = DAL.SDK.Kit.Instance.SubCategories.GetAllByCategoryId(categoryId);
            List<SubCategoryBL> mappedSubCategories = poMapper.MapSubCategoryCollection(subCategories).ToList();
            return mappedSubCategories;
        }
    }
}
