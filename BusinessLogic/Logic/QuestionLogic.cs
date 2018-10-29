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

        public QuestionDTO Add(QuestionDTO questionDTO)
        {
            try
            {
                questionDTO.Id = Guid.NewGuid();
                questionDTO.UploadDate = DateTime.Now;
                questionDTO.EditDate = questionDTO.UploadDate;
                return questionData.Add(questionDTO);
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
    }
}
