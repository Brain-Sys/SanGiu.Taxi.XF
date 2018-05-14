using System;

namespace SanGiu.Taxi.Auth
{
    public static class Authentication
    {
        public static LoginResult Check(string username, string password)
        {
            LoginResult result = new LoginResult();

            if (username == "corso" && password == "macerata")
            {
                result.IDUser = 4;
                result.Role = "SuperUser";
                result.Success = true;
            }

            if (username == "corso" && password == "macerata2")
            {
                result.IDUser = 4;
                result.Role = "Admin";
                result.Success = true;
            }

            return result;
        }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public int IDUser { get; set; }
        public string Role { get; set; }
    }
}