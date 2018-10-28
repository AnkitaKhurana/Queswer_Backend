using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class QuestionDTO
    {
        public QuestionDTO()
        {
            Tags = new List<TagDTO>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public UserDTO Author { get; set; }

        public virtual List<TagDTO> Tags { get; set; }
    }
}
