using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TagDTO
    {
        public TagDTO()
        {
            Questions = new List<QuestionDTO>();
        }
        public Guid Id { get; set; }
        public string Body { get; set; }
        public List<QuestionDTO> Questions { get;set;}
    }
}
