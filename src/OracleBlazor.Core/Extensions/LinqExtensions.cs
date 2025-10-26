using System.Linq.Expressions;

namespace OracleBlazor.Core.Entities
{
    public static class LinqExtensions{

        public static IQueryable<T> WhereIf<T>(
             this IQueryable<T> source,
             object? condition,
             Expression<Func<T, bool>> predicate)
        {
            return condition != null && !string.IsNullOrEmpty(condition.ToString()) ? source.Where(predicate) : source;
        }
        public static IQueryable<T> Page<T>(this IQueryable<T> source, OracleBlazor.Core.Filter.Pagination page = null) where T : IEntity
    {
        if (page is null) page = new();
        if (page.Size.Equals(0)) page.Size = 100;
        if (page.No.Equals(0)) page.No = 1;

        source = page.OrderDirection?.ToLower() == "asc"
            ? source.OrderBy(e => e.UpdatedAt)
            : source.OrderByDescending(e => e.UpdatedAt);

        return source
            .Skip((page.No - 1) * page.Size)
            .Take(page.Size);
    }
    }
}