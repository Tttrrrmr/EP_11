using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLogin
{
    public class LoginChecker
    {
        public static bool ValidateLogin(string login)
        {
            if (login.Length <4 ||  login.Length >5)
                return false;
            if (!login.Any(Char.IsLower))
                return false;
            if (!login.Any(Char.IsUpper))
                return false;
            if (!login.Any(Char.IsDigit))
                return false;
            if (login.Intersect("#$%^&_").Count() == 0)
                return false;

            return true;
        }
    }
}
