using FoodApp.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FoodApp.Repository.Specification;

public interface ISpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; set; }
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDesc { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public bool IsPaginationEnabled { get; set; }
}
