using Presentation.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Mapper
{
    public class AnswerMapper
    {
        public static Answer ToViewModel(AnswerDTO answerDTO)
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
                QuestionId = answerDTO.QuestionId               
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
                QuestionId = answer.QuestionId               
            };

            return answerDTO;
        }

    }
}