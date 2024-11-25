using ProjectManagementSystem.Errors;

namespace FoodApp.Errors
{
    public class CategoryErrors
    {
        public static readonly Error CategoryNotFound =
        new("Category is not found", StatusCodes.Status404NotFound);

        public static readonly Error CategoryAlreadyExists =
            new("Role Already Exists", StatusCodes.Status409Conflict);
    }
}
