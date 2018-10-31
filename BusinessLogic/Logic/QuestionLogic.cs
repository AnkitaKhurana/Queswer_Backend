using DataAccess.Data;
using Shared.DTOs;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class QuestionLogic
    {
        private QuestionData questionData = new QuestionData();


        /// <summary>
        /// Find Question
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public QuestionDTO Find(Guid questionID)
        {
            try
            {
                return questionData.Find(questionID);
            }
            catch (NoSuchQuestionFound)
            {
                throw new NoSuchQuestionFound();
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Delete Question
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public QuestionDTO Delete(Guid questionID)
        {
            try
            {
                return questionData.Delete(questionID);
            }
            catch (NoSuchQuestionFound)
            {
                throw new NoSuchQuestionFound();
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Add new question
        /// </summary>
        /// <param name="questionDTO"></param>
        /// <returns></returns>
        public QuestionDTO Add(QuestionDTO questionDTO)
        {
            try
            {
                questionDTO.Id = Guid.NewGuid();
                questionDTO.UploadDate = DateTime.Now;
                questionDTO.EditDate = questionDTO.UploadDate;
                return questionData.Add(questionDTO);
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

        /// <summary>
        /// Edit Question function
        /// </summary>
        /// <param name="questionDTO"></param>
        /// <returns></returns>
        public QuestionDTO Edit(QuestionDTO questionDTO)
        {
            try
            {
                return questionData.Edit(questionDTO);
            }
            catch (NoSuchQuestionFound)
            {
                throw new NoSuchQuestionFound();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// All questions (Page||count )
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<QuestionDTO> All(int? page, int? count)
        {
            try
            {
            
                return questionData.All(page, count);
            }
            catch
            {
                return null;
            }
        }
    }
}
