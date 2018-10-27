using DataAccess.Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class UserLogic
    {
        private UserData userData = new UserData();
        public UserDTO Find(string email, string password)
        {
            try
            {
               return userData.FindUser(email, password);
            }
            catch
            {
                return null;
            }
        }
    }
}
