
using Data.Models;
using DataAccess.Map;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class FollowData
    {
        private QueswerContext db = new QueswerContext();

        /// <summary>
        /// Follow new person
        /// </summary>
        /// <param name="follow"></param>
        /// <param name="follower"></param>
        /// <returns></returns>
        public bool Follow(Guid follow, Guid follower)
        {
            try
            {
                var existing = db.Follows.Where(x => x.FollowerId == follower && x.FollowingId == follow).FirstOrDefault();
                if (existing == null)
                {
                    Follow followRow = new Follow()
                    {
                        Id = Guid.NewGuid(),
                        FollowerId = follower,
                        FollowingId = follow
                    };
                    var result = db.Follows.Add(followRow);
                    db.SaveChanges();
                    return (result != null);
                }
                else return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Unfollow a person
        /// </summary>
        /// <param name="follow"></param>
        /// <param name="follower"></param>
        /// <returns></returns>
        public bool UnFollow(Guid follow, Guid follower)
        {
            try
            {
                var existing = db.Follows.Where(x => x.FollowerId == follower && x.FollowingId == follow).FirstOrDefault();
                if (existing != null)
                {
                    var result = db.Follows.Remove(existing);
                    db.SaveChanges();
                    return (result  != null);
                }
                else return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get user following 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserDTO> MyFollowers(Guid userID)
        {
            try
            {
                List<UserDTO> followers = new List<UserDTO>();
                var followersDB = db.Follows.Include("Following").Where(x => x.FollowerId == userID).ToList();
                foreach (var row in followersDB)
                {
                    followers.Add(UserMapper.ToDTO(row.Following));
                }
                return followers;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get user followers
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserDTO> MyFollowing (Guid userID)
        {
            try
            {
                List<UserDTO> following = new List<UserDTO>();
                var followerDB = db.Follows.Include("Follower").Where(x => x.FollowingId == userID).ToList();
                foreach (var row in followerDB)
                {
                    following.Add(UserMapper.ToDTO(row.Follower));
                }
                return following;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get followers questions
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<QuestionDTO> FollowersQuestions(Guid Id)
        {
            try
            {
                List<QuestionDTO> questions = new List<QuestionDTO>();
                var questionsDB = db.Follows.Include("Follower").Where(x => x.FollowingId == Id).ToList();
                foreach (var questionRow in questionsDB)
                {
                    foreach (var question in questionRow.Follower.Questions)
                    {
                        questions.Add(QuestionMapper.ToDTO(question));
                    }
                }
                return questions;
            }
            catch
            {
                return null;
            }
        }

    }
}
