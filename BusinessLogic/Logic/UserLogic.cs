using DataAccess.Data;
using Shared.DTOs;
using Shared.Exceptions;
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
                // Decrypt Password here 
               return userData.FindUser(email, password);
            }
            catch
            {
                return null;
            }
        }

        public UserDTO Register(UserDTO userDTO)
        {
            try
            {
                userDTO.Id = Guid.NewGuid();
                // Bycrypt Password here 
                return userData.Add(userDTO);
            }
            catch (UserAlreadyExists)
            {
                throw new UserAlreadyExists();
            }
            catch
            {
                return null;
            }
        }

    }
}
