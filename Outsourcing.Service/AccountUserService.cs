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
    public interface IAccountUserService
    {
        IEnumerable<AccountUser> GetAccountUser();
        void CreateAccountUser(AccountUser AccountUser);
        void EditAccountUser(AccountUser AccountUserToEdit);
        void DeleteAccountUser(int AccountUserId);
        void SaveAccountUser();
    }
    public class AccountUserService : IAccountUserService
    {
        #region Field
        private readonly IAccountUserRespository AccountUserRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public AccountUserService(IAccountUserRespository AccountUserRepository, IUnitOfWork unitOfWork)
        {
            this.AccountUserRepository = AccountUserRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion
        public IEnumerable<AccountUser> GetAccountUser()
        {
            var listAccountUser = AccountUserRepository.GetMany(p=>p.Deleted == false && p.IsActive == true);
            return listAccountUser;
        }
        public void CreateAccountUser(AccountUser AccountUser)
        {
            AccountUserRepository.Add(AccountUser);
            SaveAccountUser();
        }

        public void EditAccountUser(AccountUser AccountUserToEdit)
        {
            //AccountUserToEdit.LastEditedTime = DateTime.Now;
            AccountUserRepository.Update(AccountUserToEdit);
            SaveAccountUser();
        }

        public void DeleteAccountUser(int AccountUserId)
        {
            //Get AccountUser by id.
            var AccountUser = AccountUserRepository.GetById(AccountUserId);
            if (AccountUser != null)
            {
                AccountUser.Deleted = true;
                AccountUserRepository.Update(AccountUser);
                SaveAccountUser();
            }
        }

        public void SaveAccountUser()
        {
            unitOfWork.Commit();
        }
    }
}
