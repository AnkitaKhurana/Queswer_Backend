using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Question
    {
        public Question()
        {
            Tags = new List<Tag>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual List<Tag> Tags { get; set; }

    }
}
