using Microsoft.EntityFrameworkCore;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Specification;

namespace TourSite.Repository
{
    public static class SpecificationEvaluator<Entity> where Entity : class
    {
        public static IQueryable<Entity> GetQuery(IQueryable<Entity> inputQuery , ISpecifications<Entity> spec) 
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

    
            if (spec.IsPagEnable)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (currentInclude, nextInclude) => (currentInclude.Include(nextInclude)));

            // --- NEW: string includes (supports nested navigation like "Tours.Translations")
            if (spec is BaseSpecifications<Entity> bs && bs.IncludeStrings?.Any() == true)
            {
                foreach (var path in bs.IncludeStrings)
                    query = query.Include(path); // string-based Include
            }


            return query;

        }
    }
}
