using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Repositories.Contract;

namespace TourSite.Core
{
    public interface IUnitOfWork
    {
        // CompleteAsynx  - Generic repository

        Task<int> CompleteAsync();

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    }
}
