using Data.Models;
using Shared.DTOs;
using Shared.Exceptions;
using System.Linq;

namespace DataAccess.Data
{
    public class UserData
    {
        private QueswerContext db = new QueswerContext();

        public UserDTO FindUser(string email, string password)
        {
            try
            {
                User user = db.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                UserDTO userDTO = Map.UserMapper.ToDTO(user);
                return userDTO;
            }
            catch
            {
                return null;
            }

        }

        public UserDTO FindUser(string email)
        {
            try
            {
                User user = db.Users.Where(x => x.Email == email).FirstOrDefault();
                UserDTO userDTO = Map.UserMapper.ToDTO(user);
                return userDTO;
            }
            catch
            {
                return null;
            }
        }

        public UserDTO Add(UserDTO userDTO)
        {
            try
            {
                if (db.Users.Where(x => x.Email == userDTO.Email).FirstOrDefault() != null)
                {
                    throw new UserAlreadyExists();
                }
                User user = Map.UserMapper.ToDB(userDTO);
                db.Users.Add(user);
                db.SaveChanges();
                return userDTO;
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

        public UserDTO Find(string email, string password)
        {
            try
            {
                User user = db.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                if (user == null)
                {
                    throw new NoSuchUserExists();
                }
                UserDTO userDTO = Map.UserMapper.ToDTO(user);
                return userDTO;
            }
            catch (NoSuchUserExists)
            {
                throw new NoSuchUserExists();
            }
            catch
            {
                return null;
            }
        }

        public UserDTO UpdateUser(UserDTO userToUpdate)
        {

            try
            {
                User user = db.Users.Where(x => x.Email == userToUpdate.Email).FirstOrDefault();
                if (user == null)
                {
                    throw new NoSuchUserExists();
                }
                user.Firstname = userToUpdate.Firstname;
                user.Lastname = userToUpdate.Lastname;
                user.Password = userToUpdate.Password;
                db.SaveChanges();
                //Support Image here 
                User userUpdated = db.Users.Where(x => x.Email == userToUpdate.Email).FirstOrDefault();
                UserDTO userDTO = Map.UserMapper.ToDTO(userUpdated);
                return userDTO;
            }
            catch (NoSuchUserExists)
            {
                throw new NoSuchUserExists();
            }
            catch
            {
                return null;
            }
        }
    }
}
