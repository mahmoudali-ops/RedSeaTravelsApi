using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Specification
{
    public interface ISpecifications<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity,bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity,object>>> Includes { get; set; }

        public List<string> IncludeStrings { get; set; }


        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagEnable { get; set; }
    }
}
