using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;

namespace Outsourcing.Service
{
    public interface IUserService
    {

        IEnumerable<User> GetUsers();
        User GetUserById(string userId);
        void CreateUser(User user);
        void EditUser(User userToEdit);
        void DeleteUser(int userId);
        void SaveUser();
        User GetUserByUrlName(string urlName);

    }
    public class UserService : IUserService
    {
        #region Field
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Implementation for IUserService
        public IEnumerable<User> GetUsers()
        {
            var users = userRepository.GetMany(p => p.Deleted == false && p.Activated == true);
            return users;
        }
  
     

        public User GetUserById(string userId)
        {
            var user = userRepository.GetById(userId);
            return user;
        }

        public void CreateUser(User user)
        {
            userRepository.Add(user);
            SaveUser();
        }

        public void EditUser(User userToEdit)
        {
            userRepository.Update(userToEdit);
            SaveUser();
        }

        public void DeleteUser(int userId)
        {
            //Get user by id.
            var user = userRepository.GetById(userId);
            if (user != null)
            {
                user.Deleted = true;
                userRepository.Update(user);
                SaveUser();
            }
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }

      

        public User GetUserByUrlName(string urlName)
        {
            var user = userRepository.Get(b => b.LastName == urlName); 
            return user;
        }




        #endregion


    }
}
