using DAL.Entities;
using DAL.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL.Repositories
{
	public interface IUserRepository
	{
		User GetUserById(int id);
		List<User> GetAllUsers();
		User GetUserByEmailPass(string email, string pass);
		int InsertUser(User user);
		void UpdateUser(User user);
		void SetEmailPeference(int userId, int emailPreference);
        User GetUserByEmail(string email);
        void SoftDeleteUser(int userId, int isDeletedFromAuthors);
	}

	public partial class Repository : IUserRepository
	{
		#region Get
		
		public User GetUserById(int id)
		{
			User user = null;

			_dbRead.Execute(
				"UserGetById",
			new[] { 
				new SqlParameter("@Id", id), 
			},
				r => user = new User()
				{
					Id = Read<int>(r, "Id"),
					FirstName = Read<string>(r, "FirstName"),
					LastName = Read<string>(r, "LastName"),
					Email = Read<string>(r, "Email"),
					Flags = Read<UserFlags>(r, "Flags"),
					JoinDate = Read<DateTime>(r, "JoinDate"),
					UserType = Read<UserType>(r, "Id_type"),
					EmailPreference = (EmailPreference)Read<int>(r, "EmailPreference"),
				});

			if (user.Id == 0)
			{
				return null;
			}

			return user;
		}

		public User GetUserByEmail(string email)
		{
			User user = null;

			_dbRead.Execute(
				"UserGetByEmail",
			new[] { 
				new SqlParameter("@Email", email), 
			},
				r => user = new User()
				{
					Id = Read<int>(r, "Id"),
					FirstName = Read<string>(r, "FirstName"),
					LastName = Read<string>(r, "LastName"),
					Email = Read<string>(r, "Email"),
					Flags = Read<UserFlags>(r, "Flags"),
					JoinDate = Read<DateTime>(r, "JoinDate"),
					UserType = Read<UserType>(r, "Id_type"),
                    PasswordHashed = Read<string>(r, "PasswordHashed"),
				});

			return user;
		}

		public List<User> GetAllUsers()
		{
			List<User> users = new List<User>();

			_dbRead.Execute(
				"UserGetAll",
			null,
				r => users.Add(new User()
				{
					Id = Read<int>(r, "Id"),
					FirstName = Read<string>(r, "FirstName"),
					LastName = Read<string>(r, "LastName"),
					Email = Read<string>(r, "Email"),
					Flags = Read<UserFlags>(r, "Flags"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                    isDeleted = Read<int>(r, "isDeleted"),
				}));

			return users;
		}

		public User GetUserByEmailPass(string email, string pass)
		{
			User user = null;

			_dbRead.Execute(
				"UserGetByNamePass",
			new[] { 
				new SqlParameter("@Password", pass), 
				new SqlParameter("@Email", email) 
			},
				r => user = new User()
				{
					Id = Read<int>(r, "Id"),
					FirstName = Read<string>(r, "FirstName"),
					LastName = Read<string>(r, "LastName"),
					Email = Read<string>(r, "Email"),
					Flags = Read<UserFlags>(r, "Flags"),
					JoinDate = Read<DateTime>(r, "JoinDate"),
                    UserType = Read<UserType>(r, "Id_type"),
                    isDeleted = Read<int>(r, "isDeleted"),
				});

			return user;
		}

		#endregion

		#region Insert

		public int InsertUser(User user)
		{
			int userId = 0;

			_dbRead.Execute(
				"UserInsert",
			new[] { 
				new SqlParameter("@FirstName", user.FirstName), 
				new SqlParameter("@LastName", user.LastName), 
				new SqlParameter("@JoinDate", user.JoinDate), 
				new SqlParameter("@Flags", user.Flags), 
				new SqlParameter("@Email", user.Email), 
				new SqlParameter("@PasswordHashed", user.PasswordHashed), 
				new SqlParameter("@Id_typ", user.UserType), 
			}, 
				r => 
				userId = Read<int>(r, "Id")
			);
			return userId;
		}

		#endregion


		#region Update

		public void UpdateUser(User user)
		{
			_dbRead.Execute(
				"UserUpdate",
			new[] { 
				new SqlParameter("@id", user.Id), 
				new SqlParameter("@FirstName", user.FirstName), 
				new SqlParameter("@LastName", user.LastName), 
				new SqlParameter("@Flags", user.Flags), 
				new SqlParameter("@Email", user.Email), 
				new SqlParameter("@PasswordHashed", user.PasswordHashed), 
				new SqlParameter("@Id_typ", user.UserType), 
			},
			null
			);
		}

		public void SetEmailPeference(int userId, int emailPreference)
		{
			_dbRead.Execute(
				"UserUpdateEmailPreference",
			new[] { 
				new SqlParameter("@userId", userId), 
				new SqlParameter("@emailPreference", emailPreference), 
			},
			null
			);
		}

		#endregion

        public void SoftDeleteUser(int userId, int isDeletedFromAuthors)
        {
			_dbRead.Execute(
                "UserSetDeleted",
			new[] { 
				new SqlParameter("@userId", userId), 
				new SqlParameter("@accessType", isDeletedFromAuthors), 
			},
			null
			);
        }

	}
}

