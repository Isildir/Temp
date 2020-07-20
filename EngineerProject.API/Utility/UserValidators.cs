using System.Text.RegularExpressions;

namespace EngineerProject.API.Utility
{
    public static class UserValidators
    {
        private static readonly Regex emailRegex = new Regex(@"^([a-zA-Z0-9\.]+)\@([a-zA-Z0-9\.]+)\.([a-z]+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[.!@#$%^&*])(?=.{8,})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool CheckIfEmailIsCorrect(string email) => emailRegex.Match(email).Success;

        public static bool CheckIfPasswordIsCorrect(string password) => passwordRegex.Match(password).Success;
    }
}