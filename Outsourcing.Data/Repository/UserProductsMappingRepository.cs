using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using System;
using System.Linq.Expressions;
namespace Outsourcing.Data.Repository
{
    public class UserProductsMappingRepository : RepositoryBase<UserProductsMapping>, IUserProductsMappingRepository
    {
        public UserProductsMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }        
        }
    public interface IUserProductsMappingRepository : IRepository<UserProductsMapping>
    {
        
    }
}