﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Tag
    {
        public Tag()
        {
            Questions = new List<Question>();
        }
        public Guid Id { get; set; }
        public string Body { get; set; }

        public virtual List<Question> Questions { get; set; }

    }
}
