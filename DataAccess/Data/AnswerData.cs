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
    public class AnswerData
    {
        private QueswerContext db = new QueswerContext();


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
                return AnswerMapper.ToDTO(answersaved);
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
        public List<AnswerDTO> All(Guid questionId)
        {
            try{
                List<AnswerDTO> answerDTOs = new List<AnswerDTO>();
                var answers = db.Answers.Include("Author").Where(x => x.QuestionId == questionId);
                foreach(var answer in answers)
                {
                    answerDTOs.Add(AnswerMapper.ToDTO(answer));
                }

                return answerDTOs;
            }
            catch
            {
                return null;
            }
        }
    }
}
