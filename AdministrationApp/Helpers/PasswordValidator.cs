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
            // Minimalna długość hasła (np. 8 znaków)
            if (password.Length < 8)
                return false;

            // Sprawdzenie, czy hasło zawiera co najmniej jedną dużą literę
            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            // Sprawdzenie, czy hasło zawiera co najmniej jedną małą literę
            if (!Regex.IsMatch(password, @"[a-z]"))
                return false;

            // Sprawdzenie, czy hasło zawiera co najmniej jedną cyfrę
            if (!Regex.IsMatch(password, @"[0-9]"))
                return false;

            // Sprawdzenie, czy hasło zawiera co najmniej jeden znak specjalny
            if (!Regex.IsMatch(password, @"[\W_]"))
                return false;

            // Jeżeli wszystkie kryteria są spełnione, hasło jest złożone
            return true;
        }
    }
}
