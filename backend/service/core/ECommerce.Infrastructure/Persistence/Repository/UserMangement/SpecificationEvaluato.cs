using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Common.Specfication;

namespace ECommerce.Infrastructure.Persistence.Repository.UserMangement
{
    public static class SpecificationEvaluator<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        public static IQueryable<TModel> GetQuery(
            IQueryable<TModel> inputQuery,
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {

                var criteria = spec.Criteria as Expression<Func<TModel, bool>>;
                if (criteria != null)
                {
                    query = query.Where(criteria);
                }
            }

            if (spec.Includes != null)
            {
                foreach (var include in spec.Includes)
                {
                    var castedInclude = include as Expression<Func<TModel, object>>;
                    if (castedInclude != null)
                    {
                        query = query.Include(castedInclude);
                    }
                }
            }


            return query;
        }
    }
}