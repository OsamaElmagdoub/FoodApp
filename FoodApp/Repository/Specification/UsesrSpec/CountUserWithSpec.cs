using FoodApp.Data.Entities;

namespace FoodApp.Repository.Specification.UsesrSpec
{
    public class CountUserWithSpec : BaseSpecification<User>
    {
        public CountUserWithSpec(SpecParams specParams)
           : base(p => !p.IsDeleted)
        {

        }
    }
}
