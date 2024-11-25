using ProjectManagementSystem.Errors;

namespace FoodApp.Errors
{
    public class DiscountErrors
    {
        public static readonly Error DiscountPercentageNotValid =
              new ("Discount percentage must be between 1 and 100", StatusCodes.Status400BadRequest);

        public static readonly Error DatesNotValid =
              new("End date must be after the start date", StatusCodes.Status400BadRequest);

        public static readonly Error DiscountNotFound =
             new("Discount Not Found", StatusCodes.Status404NotFound);

        public static readonly Error DiscoutNotActive =
             new("Discount Not Active", StatusCodes.Status404NotFound);


        public static readonly Error ActiveDiscountAlreadyExists =
             new("Ther is Active Discount Already Applyed to this recipe ", StatusCodes.Status400BadRequest);
    }
}
