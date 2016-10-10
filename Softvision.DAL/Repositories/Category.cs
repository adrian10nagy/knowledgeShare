using DAL.Entities;
using DAL.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        void InsertCategory(string categoryName);
    }

    public partial class Repository : ICategoryRepository
	{
		#region Get

		public List<Category> GetAllCategories()
        {
            List<Category> category = new List<Category>();
            _dbRead.Execute(
                "CategoriesGetAll",
                null,
                r => category.Add( new Category()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name")
                }));
            if (category.Count != 0)
            {
                return category;
            }
            return null;
		}

		#endregion

        #region Insert

        public void InsertCategory(string categoryName)
        {
            _dbRead.ExecuteNonQuery(
                "CategoryInsert",
                new[] { new SqlParameter("@categoryName", categoryName) });
        }

        #endregion

    }
}
