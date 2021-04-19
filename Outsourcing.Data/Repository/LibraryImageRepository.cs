using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using System;
using System.Linq.Expressions;
namespace Outsourcing.Data.Repository
{
    public class LibraryImageRepository : RepositoryBase<LibraryImage>, ILibraryImageRepository
        {
        public LibraryImageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }        
        }
    public interface ILibraryImageRepository : IRepository<LibraryImage>
    {
        
    }
}