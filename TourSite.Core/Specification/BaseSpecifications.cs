using TourSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Specification;

namespace Store.Core.Specifications
{
    public class BaseSpecifications<TEntity> : ISpecifications<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set ; } =null; 
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } =new List<Expression<Func<TEntity, object>>>();

        public List<string> IncludeStrings { get; set; } = new List<string>();


        public int Take { get ; set ; }
        public int Skip { get ; set ; }
        public bool IsPagEnable { get ; set ; }

        public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
        {
             Criteria=expression;
        }

        public BaseSpecifications()
        {
            
        }

        //protected void AddTranslationInclude<TTranslation>(
        //    Expression<Func<TEntity, ICollection<TTranslation>>> includeExpression)
        //{
        //    Includes.Add(includeExpression);
        //}

        public void ApplyPag(int pageSize, int pageIndex)
        {
            IsPagEnable = true;
            Take = pageSize;
            Skip = pageSize * (pageIndex - 1);

        }
    }
}
