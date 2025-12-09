using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core;
using TourSite.Core.Repositories.Contract;
using TourSite.Repository.Data.Contexts;

namespace TourSite.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TourDbContext _context;

        private Hashtable repositiries;

        public UnitOfWork(TourDbContext context)
        {
            _context = context;
            repositiries = new Hashtable();
        }
        public async Task<int> CompleteAsync() =>  await _context.SaveChangesAsync();
       
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;

            if (!repositiries.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                repositiries.Add(type, repository);
            }
            return repositiries[type] as IGenericRepository<TEntity>;
        }
    }
}
