using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class Tag
    {
        public Tag()
        {
            Questions = new List<Question>();
        }
        public Guid Id { get; set; }
        public string Body { get; set; }
        public List<Question> Questions { get; set; }
    }
}