using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;
using TourSite.Core.Repositories.Contract;
using TourSite.Core.Specification;
using TourSite.Repository.Data.Contexts;

namespace TourSite.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly TourDbContext context;
        public GenericRepository(TourDbContext _context) 
        {
            context = _context;
        }

        public async Task<IEnumerable<TEntity>> GetAllSpecAsync(ISpecifications<TEntity> spec)
        {
            return await ApplySpecifiaction(spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdSpecAsync(ISpecifications<TEntity> spec)
        {
             var query = ApplySpecifiaction(spec);

    // ✅ بدل FirstOrDefaultAsync مباشرة، نستخدم try-catch للتأكد من أي خطأ في includes
    try
    {
        return await query.FirstOrDefaultAsync();
    }
    catch (Exception ex)
    {
        // ممكن تعمل logging هنا
        return null; // لو أي include string غلط، نرجع null
    }
        }

        public async Task AddAsync(TEntity entity)
        {
             await context.Set<TEntity>().AddAsync(entity);
        }

        public  void Update(TEntity entity)
        {
             context.Update(entity);
        }

        void IGenericRepository<TEntity>.Delete(TEntity entity)
        {
            context.Remove(entity);
        }

        private IQueryable<TEntity> ApplySpecifiaction(ISpecifications<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(context.Set<TEntity>(), spec);
        }

        public Task<int> GetCountAsync(ISpecifications<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(context.Set<TEntity>(), spec).CountAsync();
        }

        public TEntity GetByIdSpecTEntityAsync(ISpecifications<TEntity> spec)
        {
            var query = ApplySpecifiaction(spec);

            // ✅ بدل FirstOrDefaultAsync مباشرة، نستخدم try-catch للتأكد من أي خطأ في includes
            try
            {
                return  query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // ممكن تعمل logging هنا
                return null; // لو أي include string غلط، نرجع null
            }
        }

        public  async Task<TEntity> GetByIdSpecTEntityTourAsync(ISpecifications<TEntity> spec)
        {
            var query = ApplySpecifiaction(spec);

            // ✅ بدل FirstOrDefaultAsync مباشرة، نستخدم try-catch للتأكد من أي خطأ في includes
            try
            {
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // ممكن تعمل logging هنا
                return  null; // لو أي include string غلط، نرجع null
            }
        }
    }
}
