using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task06_12
{
    public static  class ValidationHelper
    {
        public static bool CheckUserName(string username)
        {
            return username.Length>=3 && username.Length<=16;
        }
        public static bool CheckPassword(string password)
        {
           if(password.Length<6 || password.Length>16)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);

            return hasUpper && hasLower && hasDigit;
        }
    }
}
