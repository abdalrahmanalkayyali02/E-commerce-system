using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Common.Specfication
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

            // 1. Apply Filtering (Criteria)
            if (spec.Criteria != null)
            {
                // We cast the Domain expression to the DataModel type
                // Note: Property names must match between Entity and Model
                var criteria = spec.Criteria as Expression<Func<TModel, bool>>;
                if (criteria != null)
                {
                    query = query.Where(criteria);
                }
            }

            // 2. Apply Includes (Eager Loading)
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

            // 3. You can add OrderBy here later if your ISpecification supports it

            return query;
        }
    }
}