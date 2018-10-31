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
    public class QuestionData
    {
        private QueswerContext db = new QueswerContext();

        /// <summary>
        /// Return all questions (page||count)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<QuestionDTO> All(int? page, int? count)
        {
            try
            {
                List<QuestionDTO> questions = new List<QuestionDTO>();
               var takePage = page ?? 1;
                var takeCount = count ?? PageConstants.DefaultPageRecordCount;
                var questionsDB = db.Questions
                               .Include("Author")   
                               .OrderBy(x=>x.UploadDate)
                               .Skip((takePage - 1) * takeCount)
                               .Take(takeCount)
                               .ToList();

                foreach(var ques in questionsDB)
                {
                    questions.Add(QuestionMapper.ToDTO(ques));
                }

                return questions;

            }
            catch 
            {
                return null;
            }
        }


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
                question.Tags = new List<Tag>();
                foreach (var tag in questionDTO.Tags)
                {
                    tag.Id = Guid.NewGuid();
                    var currentTag = db.Tags.Where(x => x.Body == tag.Body).FirstOrDefault();
                    if(currentTag == null)
                    {
                        var tagsaved = db.Tags.Add(TagMapper.ToDB(tag));
                        question.Tags.Add(tagsaved);
                    }
                    else
                    {
                        question.Tags.Add(currentTag);
                    }
                }
                
                var questionAdded = db.Questions.Add(question);
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
                List<Tag> tags  = new List<Tag>();
                foreach (var tag in questionDTO.Tags)
                {
                    tag.Id = Guid.NewGuid();
                    var currentTag = db.Tags.Where(x => x.Body == tag.Body).FirstOrDefault();
                    if (currentTag == null)
                    {
                        var tagsaved = db.Tags.Add(TagMapper.ToDB(tag));
                        tags.Add(tagsaved);
                    }
                    else
                    {
                        tags.Add(currentTag);
                    }
                }
                questionFound.Tags = tags;
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
