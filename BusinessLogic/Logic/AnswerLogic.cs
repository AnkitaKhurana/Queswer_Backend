﻿using DataAccess.Data;
using Shared.DTOs;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class AnswerLogic
    {
        private AnswerData answerData = new AnswerData();

        /// <summary>
        /// Add new Answer 
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public AnswerDTO Add(AnswerDTO answer)
        {
            try
            {
                answer.Id = Guid.NewGuid();
                answer.UploadDate = DateTime.Now;
                answer.EditDate = answer.UploadDate;
                return answerData.Add(answer);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Find all answers to a question 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public List<AnswerDTO> Find(Guid questionId)
        {
            try
            {
                return answerData.All(questionId);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Edit an answer
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public AnswerDTO Edit(AnswerDTO answer)
        {
            try
            {              
                return answerData.Edit(answer);
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

        /// <summary>
        /// Delete answer 
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public AnswerDTO Delete(Guid answerId)
        {
            try
            {
                return answerData.Delete(answerId);
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