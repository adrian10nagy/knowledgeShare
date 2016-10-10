using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class SubCategories
    {

        private static ISubCategoryRepository _repository;

        static SubCategories()
        {
            _repository =  new Repository();

        }

		#region Get

		public List<SubCategory> GetAll()
        {
            return _repository.GetAllSubCategories();
        }

        public List<SubCategory> GetAllByCategoryId(int categoryId)
        {
            return _repository.GetAllByCategoryId(categoryId);
		}

		#endregion

        #region Insert

		public void Insert(int categoryId, string subCategoryName)
        {
            _repository.Insert(categoryId, subCategoryName);
        }

        #endregion

	}
}
