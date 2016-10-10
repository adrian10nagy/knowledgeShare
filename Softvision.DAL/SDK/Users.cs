using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class Users
    {
        private static IUserRepository _repository;

        static Users()
        {
            _repository =  new Repository();
        }

		#region Get

		public User GetUserById(int id)
        {
            return _repository.GetUserById(id);            
        }

        public List<User> GetAll()
        {
            return _repository.GetAllUsers();
        }

        public User GetUserByEmailPass(string email, string pass)
        {
            return _repository.GetUserByEmailPass(email, pass);
        }

        public User GetUserByEmail(string email)
        {
            return _repository.GetUserByEmail(email);
        }

		#endregion

		#region Insert

		public int Insert(User user)
        {
            return _repository.InsertUser(user);
		}

		#endregion


        #region Update

        public void Update(User user)
        {
            _repository.UpdateUser(user);
        }

        public void SetEmailPeference(int userId, int emailPreference)
        {
            _repository.SetEmailPeference(userId, emailPreference);
        }

        #endregion

        public void SoftDeleteUser(int userId, int isDeletedFromAuthors)
        {
            _repository.SoftDeleteUser(userId, isDeletedFromAuthors);

        }
	}
}
