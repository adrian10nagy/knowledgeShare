using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ISubCategoryRepository
    {
        List<SubCategory> GetAllSubCategories();
        List<SubCategory> GetAllByCategoryId(int categoryId);
        void Insert(int categoryId, string subCategoryName);
    }

    public partial class Repository : ISubCategoryRepository
	{
		#region Get

		public List<SubCategory> GetAllSubCategories()
        {
			List<SubCategory> subCategories = new List<SubCategory>();

            _dbRead.Execute(
                "SubCategoriesGetAll",
                null,
                r => subCategories.Add(new SubCategory()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),
                    IdCategory = Read<int>(r, "Id_ctg")
                }));
            if (subCategories.Count != 0)
            {
                return subCategories;
            }

            return null;
        }

        public List<SubCategory> GetAllByCategoryId(int categoryId)
        {
            List<SubCategory> subCategories = new List<SubCategory>();

            _dbRead.Execute(
                "SubCategoriesGetAllByCategoryId",
               new[]{
                    new SqlParameter("@CategoryId", categoryId)
                },
                 r => subCategories.Add(new SubCategory()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name")
                }));

                return subCategories;
		}

		#endregion

        #region Insert

		public void Insert(int categoryId, string subCategoryName)
        {

            _dbRead.Execute(
                "SubcategoryInsert",
                new[]{
                    new SqlParameter("@subcategoryName", subCategoryName),
                    new SqlParameter("@idCategory", categoryId)
                },
                null);
        }

        #endregion
    }
}
