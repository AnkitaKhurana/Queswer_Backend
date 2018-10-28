using Data.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Map
{
    public class TagMapper
    {
        public static Tag ToDB(TagDTO tagDTO)
        {
           
            Tag tag = new Tag()
            {
               Id = tagDTO.Id,
               Body = tagDTO.Body,

            };

            return tag;
        }

        public static TagDTO ToDTO(Tag tag)
        {
            
            TagDTO tagDTO = new TagDTO()
            {
                Id = tag.Id,
                Body = tag.Body,
               
            };

            return tagDTO;
        }
    }
}
