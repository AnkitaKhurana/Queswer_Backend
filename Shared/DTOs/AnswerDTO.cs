using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class AnswerDTO
    {

        public Guid Id { get; set; }
        public string Body { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime EditDate { get; set; }
        public Guid QuestionId { get; set; }
        public QuestionDTO Question { get; set; }
        public Guid AuthorId { get; set; }
        public UserDTO Author { get; set; }
        public bool Upvoted { get; set; }
        public bool Downvoted { get; set; }
    }
}
