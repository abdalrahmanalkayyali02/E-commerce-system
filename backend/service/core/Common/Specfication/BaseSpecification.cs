using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Common.Specfication
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public BaseSpecification(Expression<Func<T, bool>> criteria) => Criteria = criteria;

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
            => Includes.Add(includeExpression);
    }
}
