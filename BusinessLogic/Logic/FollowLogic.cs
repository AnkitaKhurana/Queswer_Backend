using DataAccess.Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class FollowLogic
    {
        private FollowData followData = new FollowData();

        /// <summary>
        /// Follow user 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Follow(Guid follower, Guid following)
        {
            try
            {
                return followData.Follow(follower, following);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// UnFollow user 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool UnFollow(Guid follower, Guid following)
        {
            try
            {
                return followData.UnFollow(follower, following);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Find people User is following 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<UserDTO> MyFollowing(Guid Id)
        {
            try
            {
                return followData.MyFollowing(Id);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Find people following user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<UserDTO> MyFollowers(Guid Id)
        {
            try
            {
                return followData.MyFollowers(Id);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Questions of my following 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<QuestionDTO> Questions(Guid Id)
        {
            try
            {
                return followData.FollowersQuestions(Id);
            }
            catch
            {
                return null;
            }
        }

    }
}
