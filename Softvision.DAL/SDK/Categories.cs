using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class Categories
    {
        private static ICategoryRepository _repository;

        static Categories(){
            _repository =  new Repository();

        }

		#region Get

		public List<Category> GetAll()
        {
            return _repository.GetAllCategories();
		}

		#endregion

        #region Insert


        public void Insert(string categoryName)
        {
            _repository.InsertCategory(categoryName);
        }

        #endregion

    }

        
}
