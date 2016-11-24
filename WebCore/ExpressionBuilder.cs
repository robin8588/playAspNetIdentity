using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebCore
{
    public class Sort
    {
        public string field { get; set; }
        public string dir { get; set; }
    }

    public class Filter
    {
        public string logic { get; set; }
        public List<FilterItem> filters { get; set; }
    }

    public class FilterItem
    {
        public string Field { get; set; }
        public Op Operator { get; set; }
        public string Value { get; set; }
    }

    public class Search
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<Sort> sort { get; set; }
        public Filter filter { get; set; }
    }

    public static class OrderByName
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, string Ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = Ascending==SortOrder.asc.ToString() ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }

    public enum SortOrder
    {
        asc,
        desc
    }

    public static class FilterByName
    {
        public static IQueryable<T> FilterByField<T>(this IQueryable<T> q,List<FilterItem>filters)
        {
            var expression =  ExpressionBuilder.Build<T>(filters);
            return q.Where(expression);
        }
    }

    public class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod =
        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod =
        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });


        public static Expression<Func<T, bool>> Build<T>(IList<FilterItem> filters)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        public static Expression<Func<T,
        bool>> GetExpression<T>(IList<FilterItem> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, FilterItem filter)
        {
            MemberExpression member = Expression.Property(param, filter.Field);
            ConstantExpression constant = Expression.Constant(filter.Value);

            switch (filter.Operator)
            {
                case Op.eq:
                    return Expression.Equal(member, constant);

                case Op.gt:
                    return Expression.GreaterThan(member, constant);

                case Op.gte:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Op.lt:
                    return Expression.LessThan(member, constant);

                case Op.lte:
                    return Expression.LessThanOrEqual(member, constant);

                case Op.contains:
                    return Expression.Call(member, containsMethod, constant);

                case Op.startswith:
                    return Expression.Call(member, startsWithMethod, constant);

                case Op.endswith:
                    return Expression.Call(member, endsWithMethod, constant);
            }

            return null;
        }

        private static BinaryExpression GetExpression<T>
        (ParameterExpression param, FilterItem filter1, FilterItem filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }

    public enum Op
    {
        eq,
        gt,
        lt,
        gte,
        lte,
        contains,
        startswith,
        endswith
    }
}
