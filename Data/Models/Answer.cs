using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Answer
    {
        public Answer()
        {
            Voters = new List<Vote>();
        }
        public Guid Id { get; set; }
        public string Body { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime EditDate { get; set; }

        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }

        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<Vote> Voters { get; set; }

    }
}
