using Common.Specfication;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository
{
    public class SpecificationEvaluator<TEntity, TModel> where TModel : class
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TModel> inputQuery,
            ISpecification<TEntity> spec,
            Func<TModel, TEntity> mapper)
        {

            var entities = inputQuery.AsEnumerable().Select(mapper).AsQueryable();

            if (spec.Criteria != null)
            {
                entities = entities.Where(spec.Criteria);
            }

            return entities;
        }
    }

}
