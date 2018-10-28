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


    }
}
