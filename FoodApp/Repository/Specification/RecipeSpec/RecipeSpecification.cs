using FoodApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Repository.Specification.RecipeSpec;

public class RecipeSpecification : BaseSpecification<Recipe>
{
    public RecipeSpecification(int id)
        :base(r=>r.Id == id)
    {
        Includes.Add(p => p.Include(p => p.Category));
        Includes.Add(p => p.Include(p => p.RecipeDiscounts).ThenInclude(rd=>rd.Discount));


    }
    public RecipeSpecification(SpecParams spec)
    {
        Includes.Add(p => p.Include(p => p.Category));
        Includes.Add(p => p.Include(p => p.RecipeDiscounts).ThenInclude(rd=>rd.Discount));


        if (!string.IsNullOrEmpty(spec.Search))
        {
            Criteria = p => p.Name.ToLower().Contains(spec.Search.ToLower());
        }

        if (!string.IsNullOrEmpty(spec.Sort))
        {
            switch (spec.Sort.ToLower())
            {
                case "Name":
                    AddOrderBy(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.Price);
                    break;
            }
        }

        ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
    }
}