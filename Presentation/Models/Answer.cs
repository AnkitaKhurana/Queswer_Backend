using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class Answer
    {

        [Required(ErrorMessage = "Body is required")]
        [StringLength(100, MinimumLength = 1,
       ErrorMessage = "Firstname Should be minimum 1 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string Body { get; set; }

        public Guid Id { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime EditDate { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AuthorId { get; set; }

    }
}