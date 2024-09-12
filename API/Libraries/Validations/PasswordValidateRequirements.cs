using System.Text.RegularExpressions;

namespace API.Libraries.Validations
{
    public class PasswordValidateRequirements
    {
        public static bool IsValid(string password)
        {
            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, @"[\W_]"))
                return false;

            return true;
        }

        public static string RequirementsString()
        {
            string requirements = "The password must be at least 8 characters, with a capital letter and a special character.";

            return requirements;
        }

        public static bool PasswordConfirmationIsEqual(string password, string confirmPassword)
        {
            if (password == confirmPassword) return true;
            return false;
        }
    }
}
