using ProjectManagementSystem.Errors;

namespace FoodApp.Errors
{
    public class RoleErrors
    {
        public static readonly Error RoleNotFound =
        new("Role is not found", StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedRole =
            new("Another role with the same name is already exists", StatusCodes.Status409Conflict);

        public static readonly Error RoleAlreadyExists =
            new("Role Already Exists", StatusCodes.Status409Conflict);

        public static readonly Error UserNotAssignedToThatRole =
            new("UserNotAssignedToThatRole", StatusCodes.Status400BadRequest);

        public static readonly Error RoleNotAssigned =
           new("Role Not Assigned", StatusCodes.Status400BadRequest);
    }
}
