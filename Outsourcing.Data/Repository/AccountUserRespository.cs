using System;
using System.Collections.Generic;
using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using System;
using System.Linq.Expressions;

namespace Outsourcing.Data.Repository
{
    public class AccountUserRespository : RepositoryBase<AccountUser>, IAccountUserRespository
    {
        public AccountUserRespository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IAccountUserRespository : IRepository<AccountUser>
    {

    }
}
