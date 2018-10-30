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

    }
}
