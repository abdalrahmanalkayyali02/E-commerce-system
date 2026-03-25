using Common.Specfication;
using Microsoft.EntityFrameworkCore;

public static class SpecificationEvaluator<TEntity> where TEntity : class
{
    public static IQueryable<TEntity> GetQuery(
        IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.Includes != null)
        {
            query = spec.Includes.Aggregate(query,
                (current, include) => current.Include(include));
        }

        return query;
    }
}