using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Constants
{
    public static class AuthConstants
    {
      
        public static string ACCESS_TOKEN= "access_token";
        public static string GenerateRequest(string email, string password)
        {
            return "grant_type=password&email=" + email + "&password=" + password;
        }

    }
}
