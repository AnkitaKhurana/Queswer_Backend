using Data.Models;
using DataAccess.Map;
using Shared.Constants;
using Shared.DTOs;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class AnswerData
    {
        private QueswerContext db = new QueswerContext();

        private bool CheckUpvote(Guid currentUser,Guid currentAnswer)
        {
            try
            {
                var found = db.Voters.Where(x => x.UserId == currentUser && x.AnswerId == currentAnswer && x.Status == (int)EntityConstants.VOTE.Upvote);
                if (found == null)
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }

        private bool CheckDownvote(Guid currentUser, Guid currentAnswer)
        {
            try
            {
                var found = db.Voters.Where(x => x.UserId == currentUser && x.AnswerId == currentAnswer && x.Status == (int)EntityConstants.VOTE.Downvote);
                if (found == null)
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Add answer to db
        /// </summary>
        /// <param name="answerDTO"></param>
        /// <returns></returns>
        public AnswerDTO Add(AnswerDTO answerDTO)
        {
            try
            {
               
                var answerToSave = db.Answers.Add(AnswerMapper.ToDB(answerDTO));
                db.SaveChanges();
                var answersaved = db.Answers.Include("Author").Where(x => x.Id == answerToSave.Id).FirstOrDefault();
                AnswerDTO answerToReturn = AnswerMapper.ToDTO(answersaved);
                answerToReturn.Upvoted = false;
                answerToReturn.Downvoted = false;
                return answerToReturn;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get all answers of a question 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public List<AnswerDTO> All(Guid questionId, Guid currentUser)
        {
            try{
                List<AnswerDTO> answerDTOs = new List<AnswerDTO>();
                var answers = db.Answers.Include("Author").Where(x => x.QuestionId == questionId).ToList();
                foreach(var answer in answers)
                {
                    AnswerDTO temp = AnswerMapper.ToDTO(answer);
                    temp.Upvoted = CheckUpvote(currentUser, answer.Id);
                    temp.Downvoted = CheckDownvote(currentUser, answer.Id);
                    answerDTOs.Add(temp);
                }

                return answerDTOs;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Edit answer 
        /// </summary>
        /// <param name="answerDTO"></param>
        /// <returns></returns>
        public  AnswerDTO Edit(AnswerDTO answerDTO, Guid currentUser)
        {
            try
            {
                AnswerDTO answerupdated = new AnswerDTO();
                var answerFound = db.Answers.Where(x => x.Id == answerDTO.Id).FirstOrDefault();
                if (answerFound == null)
                {
                    throw new NoSuchQuestionFound();
                }
                answerFound.Body = answerDTO.Body;
                answerFound.EditDate = DateTime.Now;
                answerupdated = AnswerMapper.ToDTO(answerFound);
                answerupdated.Upvoted = CheckUpvote(currentUser, answerFound.Id);
                answerupdated.Downvoted = CheckDownvote(currentUser, answerFound.Id);
                db.SaveChanges();
                return answerupdated;
            }
            catch (NoSuchAnswerFound)
            {
                throw new NoSuchQuestionFound();
            }
            catch
            {
                return null;
            }

        }
    
        /// <summary>
        /// Delete User from database 
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public AnswerDTO Delete(Guid answerId)
        {
            try
            {
                Answer answer = db.Answers.Where(x => x.Id == answerId).FirstOrDefault();
                if (answer == null)
                {
                    throw new NoSuchAnswerFound();
                }
                AnswerDTO answerDTO = AnswerMapper.ToDTO(answer);
                db.Answers.Remove(answer);
                db.SaveChanges();
                return answerDTO;
            }
            catch (NoSuchAnswerFound)
            {
                throw new NoSuchAnswerFound();
            }
            catch
            {
                return null;
            }

        }
    }
}
