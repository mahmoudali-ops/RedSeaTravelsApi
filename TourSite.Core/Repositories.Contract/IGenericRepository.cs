using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Specification;

namespace TourSite.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllSpecAsync(ISpecifications<TEntity> spec);
        Task<TEntity> GetByIdSpecAsync(ISpecifications<TEntity> spec);
        TEntity GetByIdSpecTEntityAsync(ISpecifications<TEntity> spec);
        Task<TEntity> GetByIdSpecTEntityTourAsync(ISpecifications<TEntity> spec);
        Task<int> GetCountAsync(ISpecifications<TEntity> spec);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
