using Presentation.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Mapper
{
    public class RegisterUserMapper
    {
        public static UserRegister ToViewModel(UserDTO userDTO)
        {
            UserRegister user = new UserRegister()
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                Firstname = userDTO.Firstname,
                Lastname = userDTO.Lastname              
            };
            return user;
        }

        public static UserDTO ToDTO(UserRegister user)
        {
            UserDTO userDTO = new UserDTO()
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                Password = user.Password,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                FollowCount = 0,
                Image = null
            };
            return userDTO;
        }
    }
}