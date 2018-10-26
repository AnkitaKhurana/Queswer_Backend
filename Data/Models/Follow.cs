using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Follow
    {
        public Guid Id { get; set; }

        [ForeignKey("Follower")]
        public Guid FollowerId { get; set; }
        public virtual User Follower { get; set; }

        [ForeignKey("Following")]
        public Guid FollowingId { get; set; }
        public virtual User Following { get; set; }
    }
}
