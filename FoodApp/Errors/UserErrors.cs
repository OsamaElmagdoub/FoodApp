using ProjectManagementSystem.Errors;

namespace FoodApp.Errors
{
    public class UserErrors
    {
        public static readonly Error InvalidCredentials =
       new("Invalid email/password", StatusCodes.Status401Unauthorized);

        public static readonly Error InvalidEmail =
           new("Invalid email", StatusCodes.Status404NotFound);

        public static readonly Error UserNotFound =
           new("User Not Found", StatusCodes.Status404NotFound);

        public static readonly Error InvalidCurrentPassword =
            new("Current password is incorrect.", StatusCodes.Status400BadRequest);

        public static readonly Error UserNotVerified =
            new("user is not verified", StatusCodes.Status400BadRequest);

        public static readonly Error InvalidResetCode =
            new("Invalid reset code", StatusCodes.Status400BadRequest);

        public static readonly Error UserAlreadyExists =
            new("User Already Exists", StatusCodes.Status409Conflict);

        public static readonly Error UserDoesntCreated =
            new("User Doesnt Created", StatusCodes.Status409Conflict);

        public static readonly Error FailedToSendVerificationEmail =
            new("Failed to send verification email", StatusCodes.Status409Conflict);

        public static readonly Error OTPExpired =
            new("OTP Is Expired", StatusCodes.Status400BadRequest);

        public static readonly Error PasswordsDoNotMatch =
            new("Passwords Do Not Match", StatusCodes.Status400BadRequest);

        public static readonly Error InvalidOTP =
            new("Invalid OTP", StatusCodes.Status400BadRequest);

        public static readonly Error NoRefreshTokensFound =
            new("No Refresh Tokens To This User Found", StatusCodes.Status400BadRequest);

        public static readonly Error InvalidRefreshToken =
              new("Invalid Refresh Token", StatusCodes.Status400BadRequest);


        public static readonly Error TokenIsRequired =
              new("Token Is Required", StatusCodes.Status400BadRequest);
 

        public static readonly Error NoLoggedInUserFound =
              new("There Is No Logged User", StatusCodes.Status400BadRequest);


        public static readonly Error UserNotAuthenticated =
              new("You are not authenticated", StatusCodes.Status400BadRequest);
 

        public static readonly Error EmailIsAlreadyVerified =
              new("Email Is Already Verified", StatusCodes.Status400BadRequest);

    }

}
