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
    public interface IUserProductsMappingService
    {

        UserProductsMapping GetByUserId(string userId);
        IEnumerable<UserProductsMapping> GetUserProductsMappings();
        UserProductsMapping GetUserProductsMappingById(int orderId);
        void CreateUserProductsMapping(UserProductsMapping order);
        void EditUserProductsMapping(UserProductsMapping orderToEdit);
        void DeleteUserProductsMapping(int orderId);
        void SaveUserProductsMapping();
        IEnumerable<ValidationResult> CanAddUserProductsMapping(UserProductsMapping order);

    }
    public class UserProductsMappingService : IUserProductsMappingService
    {
        #region Field
        private readonly IUserProductsMappingRepository orderRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public UserProductsMappingService(IUserProductsMappingRepository orderRepository, IUnitOfWork unitOfWork)
        {
            this.orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public UserProductsMapping GetByUserId(string userId)
        {
            var product = orderRepository.Get(p => p.UserId == userId && !p.IsDeleted);
            return product;
        }
        public IEnumerable<UserProductsMapping> GetUserProductsMappings()
        {
            var orders = orderRepository.GetAll().OrderByDescending(b => b.CreateDate);
            return orders;
        }

        public UserProductsMapping GetUserProductsMappingById(int orderId)
        {
            var order = orderRepository.GetById(orderId);
            return order;
        }

        public void CreateUserProductsMapping(UserProductsMapping order)
        {
            orderRepository.Add(order);
            SaveUserProductsMapping();
        }

        public void EditUserProductsMapping(UserProductsMapping orderToEdit)
        {
            orderRepository.Update(orderToEdit);
            SaveUserProductsMapping();
        }

        public void DeleteUserProductsMapping(int orderId)
        {
            //Get order by id.
            var order = orderRepository.GetById(orderId);
            if (order != null)
            {
                orderRepository.Delete(order);
                SaveUserProductsMapping();
            }
        }

        public void SaveUserProductsMapping()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddUserProductsMapping(UserProductsMapping order)
        {

            return null;
        }

        #endregion
    }
}
