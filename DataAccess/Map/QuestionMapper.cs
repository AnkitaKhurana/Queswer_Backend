using Data.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Map
{
    public class QuestionMapper
    {
        public static Question ToDB(QuestionDTO questionDTO)
        {
            List<Tag> tags = new List<Tag>();
            foreach (var tagItem in questionDTO.Tags)
            {
                tags.Add(TagMapper.ToDB(tagItem));

            }
            Question question = new Question()
            {
                Id = questionDTO.Id,
                Title = questionDTO.Title,
                Description = questionDTO.Description,
                AuthorId = questionDTO.Author.Id,
                Image = questionDTO.Image,
                Tags = tags               
            };

            return question;
        }

        public static QuestionDTO ToDTO(Question question)
        {
            List<TagDTO> tags = new List<TagDTO>();
            foreach (var tagItem in question.Tags)
            {
                tags.Add(TagMapper.ToDTO(tagItem));

            }
            QuestionDTO questionDTO = new QuestionDTO()
            {
                Id = question.Id,
                Title = question.Title,
                Description = question.Description,
                Image = question.Image,
                Tags = tags,
                Author = UserMapper.ToDTO(question.Author),
            };

            return questionDTO;
        }

    }
}
