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
    public class UserCrud
    {

        public List<UserBL> GetAll()
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<User> users = DAL.SDK.Kit.Instance.Users.GetAll();
            List<UserBL> mappedUsers = poMapper.MapUserCollection(users).ToList();
            return mappedUsers;
        }

        public UserBL GetByEmailPass(string email, string pass)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            User user = DAL.SDK.Kit.Instance.Users.GetUserByEmailPass(email, pass);
            UserBL mappedUser = poMapper.MapUser(user);
            return mappedUser;
        }

        public UserBL GetByEmail(string email)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            User user = DAL.SDK.Kit.Instance.Users.GetUserByEmail(email);
            UserBL mappedUser = poMapper.MapUser(user);
            return mappedUser;
        }

        public UserBL GetById(int id)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            User user = DAL.SDK.Kit.Instance.Users.GetUserById(id);
            UserBL mappedUser = poMapper.MapUser(user);
            return mappedUser;
        }

        public int Insert(UserBL user)
        {
            BLToDALMapper poMapper = new BLToDALMapper();
            User mappedUser = poMapper.MapUser(user);
            int userId = DAL.SDK.Kit.Instance.Users.Insert(mappedUser);
            return userId;
        }

        public void Update(UserBL user)
        {
            BLToDALMapper poMapper = new BLToDALMapper();
            User mappedUser = poMapper.MapUser(user);
            DAL.SDK.Kit.Instance.Users.Update(mappedUser);
        }

        public void SetEmailPeference(int userId, int emailPreference)
        {
            DAL.SDK.Kit.Instance.Users.SetEmailPeference(userId, emailPreference);
        }

        public void RemoveFromAuthors(int userId, int accessType)
        {
            DAL.SDK.Kit.Instance.Users.SoftDeleteUser(userId, accessType);
        }
    }
}
