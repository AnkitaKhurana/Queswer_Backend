using Data.Models;
using DataAccess.Map;
using Shared.Constants;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class VoteData
    {
        private QueswerContext db = new QueswerContext();

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
                AnswerDTO answerDTO = new AnswerDTO();
                Answer answerFound = db.Answers.Where(x => x.Id == answerId).FirstOrDefault();

                var voteColumn = db.Voters.Where(x => x.AnswerId == answerId && x.UserId == voterId).FirstOrDefault();
                if (voteColumn == null)
                {
                    answerFound.UpvoteCount++;
                    Vote newVote = new Vote()
                    {
                        Id = Guid.NewGuid(),
                        UserId = voterId,
                        AnswerId = answerId,
                        Status = (int)EntityConstants.VOTE.Upvote
                    };
                    db.Voters.Add(newVote);
                }
                else
                {
                    if (voteColumn.Status == (int)EntityConstants.VOTE.Upvote)
                    {
                        answerDTO = AnswerMapper.ToDTO(answerFound);
                        return answerDTO;
                    }
                    else if (voteColumn.Status == (int)EntityConstants.VOTE.Downvote)
                    {
                        answerFound.UpvoteCount++;
                        answerFound.DownvoteCount--;
                        voteColumn.Status = (int)EntityConstants.VOTE.Upvote;
                    }
                }
                db.SaveChanges();
                answerDTO = AnswerMapper.ToDTO(answerFound);
                answerDTO.Upvoted = true;
                answerDTO.Downvoted = false;
                return answerDTO;
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
                AnswerDTO answerDTO = new AnswerDTO();
                Answer answerFound = db.Answers.Where(x => x.Id == answerId).FirstOrDefault();

                var voteColumn = db.Voters.Where(x => x.AnswerId == answerId && x.UserId == voterId).FirstOrDefault();
                if (voteColumn == null)
                {
                    answerFound.UpvoteCount++;
                    Vote newVote = new Vote()
                    {
                        Id = Guid.NewGuid(),
                        UserId = voterId,
                        AnswerId = answerId,
                        Status = (int)EntityConstants.VOTE.Downvote
                    };
                    db.Voters.Add(newVote);
                }
                else
                {
                    if (voteColumn.Status == (int)EntityConstants.VOTE.Downvote)
                    {
                        answerDTO = AnswerMapper.ToDTO(answerFound);
                        return answerDTO;
                    }
                    else if (voteColumn.Status == (int)EntityConstants.VOTE.Upvote)
                    {
                        answerFound.UpvoteCount--;
                        answerFound.DownvoteCount++;
                        voteColumn.Status = (int)EntityConstants.VOTE.Downvote;
                    }
                }
                db.SaveChanges();
                answerDTO = AnswerMapper.ToDTO(answerFound);
                answerDTO.Upvoted = false;
                answerDTO.Downvoted = true;
                return answerDTO;
            }
            catch
            {
                return null;
            }

        }

    }
}
