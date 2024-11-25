namespace FoodApp.Helper
{
    public static class CookieHelper
    {
        public static void SetRefreshTokenCookie(HttpResponse response, string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                Expires = DateTime.UtcNow.AddDays(7)
            };

            response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        public static string GetRefreshTokenCookie(HttpRequest request)
        {
            return request.Cookies["refreshToken"];
        }
    }

}
