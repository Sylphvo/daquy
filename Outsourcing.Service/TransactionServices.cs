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

    public interface ITransactionService
    {

        IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> GetHomePageTransactions();
        IEnumerable<Transaction> GetTransactionByCategoryId(int id);
  
        Transaction GetTransactionById(int TransactionId);
        void CreateTransaction(Transaction Transaction);
        void EditTransaction(Transaction TransactionToEdit);
        void DeleteTransaction(int TransactionId);
        void SaveTransaction();
    
    }
    public class TransactionService : ITransactionService
    {
        #region Field
        private readonly ITransactionRepository TransactionRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public TransactionService(ITransactionRepository TransactionRepository, IUnitOfWork unitOfWork)
        {
            this.TransactionRepository = TransactionRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        public IEnumerable<Transaction> GetTransactions()
        {
            var Transactions = TransactionRepository.GetAll().OrderByDescending(b => b.DateCreate);
            return Transactions;
        }
        public IEnumerable<Transaction> Get3TransactionsPosition()
        {
            var Transactions = TransactionRepository.GetAll().OrderByDescending(b => b.DateCreate);
            return Transactions;
        }
        public IEnumerable<Transaction> GetHomePageTransactions()
        {
            var Transactions = TransactionRepository.GetAll().OrderByDescending(b => b.DateCreate);

            return Transactions;
        }
        public IEnumerable<Transaction> GetTransactionByCategoryId(int userId)
        {
            var Transactions = TransactionRepository.GetMany(b => b.UserId== userId).
                OrderByDescending(b => b.DateCreate);
            return Transactions;
        }
        public Transaction GetTransactionById(int TransactionId)
        {
            var Transaction = TransactionRepository.GetById(TransactionId);
            return Transaction;
        }

        public void CreateTransaction(Transaction Transaction)
        {
            TransactionRepository.Add(Transaction);
            SaveTransaction();
        }

        public void EditTransaction(Transaction TransactionToEdit)
        {
            TransactionRepository.Update(TransactionToEdit);
            SaveTransaction();
        }

        public void DeleteTransaction(int TransactionId)
        {
            //Get Transaction by id.
            var Transaction = TransactionRepository.GetById(TransactionId);
            //var Transaction = TransactionRepository.GetAll().Where(p=>p.)
            if (Transaction != null)
            {
                Transaction.Type = 6;
                TransactionRepository.Update(Transaction);
                SaveTransaction();
            }
        }

        public void SaveTransaction()
        {
            unitOfWork.Commit();
        }

      

    }
}
