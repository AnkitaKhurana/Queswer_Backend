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


        /// <summary>
        /// Find user by email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserDTO Find(string email)
        {
            try
            {
                return userData.FindUser(email);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Find user by email and password 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Register new user 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update User Details 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserDTO Update(UserDTO user)
        {
            try
            {
                //Bycrypt password here
                return userData.UpdateUser(user);
            }
            catch
            {
                return null;
            }
        }

    }
}
