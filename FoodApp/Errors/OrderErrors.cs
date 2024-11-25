using ProjectManagementSystem.Errors;

namespace FoodApp.Errors;

public class OrderErrors
{
    public static readonly Error OrderNotFound =
    new("Order not found.", StatusCodes.Status404NotFound);
}
