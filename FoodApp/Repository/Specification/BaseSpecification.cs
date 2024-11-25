using FoodApp.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FoodApp.Repository.Specification;

public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; set; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDesc { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 0;
    public bool IsPaginationEnabled { get; set; } = false;

    public BaseSpecification()
    {

    }

    public BaseSpecification(Expression<Func<T, bool>> CriteriaExpression)
    {
        Criteria = CriteriaExpression;
    }

    public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
    {
        OrderBy = OrderByExpression;
    }

    public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
    {
        OrderByDesc = OrderByDescExpression;
    }

    public void ApplyPagination(int skip, int take)
    {
        IsPaginationEnabled = true;
        Skip = skip;
        Take = take;
    }
}