using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdministrationApp.Helpers
{
    public class PasswordValidator
    {
        public static bool ValidatePassword(string password)
        {
            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, @"[a-z]"))
                return false;

            if (!Regex.IsMatch(password, @"[0-9]"))
                return false;

            if (!Regex.IsMatch(password, @"[\W_]"))
                return false;

            return true;
        }
    }
}
