using DataAccess.Data;
using Shared.Constants;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class VoteLogic
    {
        private VoteData voteData = new VoteData();


        /// <summary>
        /// Upvote an answer
        /// </summary>
        /// <param name="voterId"></param>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public AnswerDTO Upvote(Guid voterId, Guid answerId)
        {
            try
            {
                return voteData.Upvote(voterId, answerId);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Downvote an answer
        /// </summary>
        /// <param name="voterId"></param>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public AnswerDTO Downvote(Guid voterId, Guid answerId)
        {
            try
            {
                return voteData.Downvote(voterId, answerId);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Un Up vote
        /// </summary>
        /// <param name="voterId"></param>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public AnswerDTO UnUpvote(Guid voterId, Guid answerId)
        {
            try
            {
                return voteData.UnUpVote(voterId, answerId);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Un Down vote
        /// </summary>
        /// <param name="voterId"></param>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public AnswerDTO UnDownvote(Guid voterId, Guid answerId)
        {
            try
            {
                return voteData.UnDownVote(voterId, answerId);
            }
            catch
            {
                return null;
            }
        }


    }
}
