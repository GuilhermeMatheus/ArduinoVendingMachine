using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.EF.Extensions
{
    public static class LinqExpressions
    {
        public static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(
            Expression<Func<TElement, TValue>> valueSelector,
            IEnumerable<TValue> values
        )
        {
            if (null == valueSelector)
                throw new ArgumentNullException("valueSelector");

            if (null == values)
                throw new ArgumentNullException("values");

            var p = valueSelector.Parameters.Single();

            if (!values.Any())
                return e => false;

            var equals = values.Select(
                value => 
                    (Expression)Expression.Equal(
                                    valueSelector.Body,
                                    Expression.Constant(value, typeof(TValue))
                                )
            );

            var body = equals.Aggregate(
                (accumulate, equal) => 
                    Expression.Or(accumulate, equal)
            );

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }
    }
}
