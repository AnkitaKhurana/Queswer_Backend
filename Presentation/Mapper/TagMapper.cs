using Presentation.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Mapper
{
    public class TagMapper
    {
        public static Tag ToViewModel(TagDTO tagDTO)
        {
            List<Question> question = new List<Question>();
            foreach (var ques in tagDTO.Questions)
            {
                question.Add(QuestionMapper.ToViewModel(ques));

            }
            Tag tag = new Tag()
            {
                Id = tagDTO.Id,
                Body = tagDTO.Body,
                Questions = question

            };

            return tag;
        }

        public static TagDTO ToDTO(Tag tag)
        {
            List<QuestionDTO> question = new List<QuestionDTO>();
            foreach (var ques in tag.Questions)
            {
                question.Add(QuestionMapper.ToDTO(ques));

            }
            TagDTO tagDTO = new TagDTO()
            {
                Id = tag.Id,
                Body = tag.Body,
                Questions = question

            };

            return tagDTO;
        }
    }
}