using Presentation.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Mapper
{
    public class UserMapper
    {
        public static User ToViewModel(UserDTO userDTO)
        {
            User user = new User()
            {
                Id = userDTO.Id,
                Email = userDTO.Email,
                //Password = userDTO.Password,
                Firstname = userDTO.Firstname,
                Lastname = userDTO.Lastname,
                FollowCount = userDTO.FollowCount,
                Image = userDTO.Image
            };
            return user;
        }

        public static UserDTO ToDTO(User user)
        {
            UserDTO userDTO = new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                FollowCount = user.FollowCount,
                Image = user.Image
            };
            return userDTO;
        }
    }
}