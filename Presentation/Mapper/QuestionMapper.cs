using Presentation.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Mapper
{
    public class QuestionMapper
    {
        public static Question ToViewModel(QuestionDTO questionDTO)
        {
          
            Question question = new Question()
            {
                Id = questionDTO.Id,
                Title = questionDTO.Title,
                Description = questionDTO.Description,
                Image = questionDTO.Image,              
                Author = UserMapper.ToViewModel(questionDTO.Author),
            };

            return question;
        }

        public static QuestionDTO ToDTO(Question question)
        {
          
            QuestionDTO questionDTO = new QuestionDTO()
            {
                Id = question.Id,
                Title = question.Title,
                Description = question.Description,
                Image = question.Image,               
            };

            return questionDTO;
        }
    }
}