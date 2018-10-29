using Data.Models;
using DataAccess.Map;
using Shared.DTOs;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class QuestionData
    {
        private QueswerContext db = new QueswerContext();

        /// <summary>
        /// Find Question by Id 
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        public QuestionDTO Find(Guid QuestionId)
        {
            try
            {
                Question question = db.Questions.Where(x => x.Id == QuestionId).FirstOrDefault();
                if (question == null)
                {
                    throw new NoSuchQuestionFound();
                }
                QuestionDTO questionDTO = QuestionMapper.ToDTO(question);
                return questionDTO;
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
        /// Add new Question 
        /// </summary>
        /// <param name="questionDTO"></param>
        /// <returns></returns>
        public QuestionDTO Add(QuestionDTO questionDTO)
        {
            try
            {
                Question question = QuestionMapper.ToDB(questionDTO);
                var questionAdded = db.Questions.Add(question);

                foreach (var tag in questionAdded.Tags)
                {
                    tag.Id = Guid.NewGuid();
                    db.Tags.Add(tag);
                }
                db.SaveChanges();
                var questionFound = db.Questions
                 .Include("Author")
                 .Where(s => s.Id == questionAdded.Id)
                 .FirstOrDefault();
                return QuestionMapper.ToDTO(questionFound);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Delete question by Id 
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        public QuestionDTO Delete(Guid QuestionId)
        {
            try
            {
                Question question = db.Questions.Where(x => x.Id == QuestionId).FirstOrDefault();
                if (question == null)
                {
                    throw new NoSuchQuestionFound();
                }
                QuestionDTO questionDTO = QuestionMapper.ToDTO(question);
                db.Questions.Remove(question);
                db.SaveChanges();
                return questionDTO;
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
        /// Edit question
        /// </summary>
        /// <param name="questionDTO"></param>
        /// <returns></returns>
        public QuestionDTO Edit(QuestionDTO questionDTO)
        {
            try
            {
                QuestionDTO questionUpdated = new QuestionDTO();
                var questionFound = db.Questions.Where(x => x.Id == questionDTO.Id).FirstOrDefault();
                if(questionFound == null)
                {
                    throw new NoSuchQuestionFound();

                }
                questionFound.Description = questionDTO.Description;
                questionFound.Title = questionDTO.Title;
                questionFound.Tags = new List<Tag>();
                foreach(var tag in questionDTO.Tags)
                {
                    questionFound.Tags.Add(TagMapper.ToDB(tag));
                }
                questionFound.Image = questionDTO.Image;
                questionFound.EditDate = DateTime.Now;
                questionUpdated = QuestionMapper.ToDTO(questionFound);
                db.SaveChanges();
                return questionUpdated;
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
