using System.Data.Entity;

namespace Data.Models
{
    public class QueswerContext : DbContext
    {
        public QueswerContext() : base("QueswerContext")
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Vote> Voters { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
    }
}
