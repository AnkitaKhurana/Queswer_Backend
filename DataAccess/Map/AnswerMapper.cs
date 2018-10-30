using Data.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Map
{
    public class AnswerMapper
    {
        public static Answer ToDB(AnswerDTO answerDTO)
        {

            Answer answer = new Answer()
            {
                Id = answerDTO.Id,
                Body = answerDTO.Body,
                AuthorId = answerDTO.AuthorId,
                DownvoteCount = answerDTO.DownvoteCount,
                UpvoteCount = answerDTO.UpvoteCount,
                UploadDate = answerDTO.UploadDate,
                EditDate = answerDTO.EditDate,
                QuestionId = answerDTO.QuestionId,
               
            };

            return answer;
        }

        public static AnswerDTO ToDTO(Answer answer)
        {


            AnswerDTO answerDTO = new AnswerDTO()
            {
                Id = answer.Id,
                Body = answer.Body,
                AuthorId = answer.AuthorId,
                DownvoteCount = answer.DownvoteCount,
                UpvoteCount = answer.UpvoteCount,
                UploadDate = answer.UploadDate,
                EditDate = answer.EditDate,
                QuestionId = answer.QuestionId,
                Author = UserMapper.ToDTO(answer.Author),
                //Question = QuestionMapper.ToDTO(answer.Question)
            };

            return answerDTO;
        }

    }
}
