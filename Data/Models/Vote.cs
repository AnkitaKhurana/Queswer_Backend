using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Vote
    {
        public Guid Id { get; set; }
        public int Status { get; set; } //ENUM {UPVOTE : DOWNVOTE}


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey("Answer")]
        public Guid AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
        
    }
}
