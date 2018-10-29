using Data.Models;
using DataAccess.Map;
using Shared.DTOs;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class TagData
    {
        private QueswerContext db = new QueswerContext();


        /// <summary>
        /// Find All Tags 
        /// </summary>
        /// <returns></returns>
        public List<TagDTO>FindAll()
        {
            try
            {
                List<TagDTO> tags = new List<TagDTO>();
                var dbTags = db.Tags.ToList();
                foreach(var tag in dbTags)
                {
                    TagDTO currentTag = new TagDTO()
                    {
                        Id = tag.Id,
                        Body = tag.Body
                    };
                    tags.Add(currentTag);
                }
                return tags;
            }
            catch
            {
                return null;
            }

        }


        /// <summary>
        /// Find tag by Id 
        /// </summary>
        /// <param name="TagId"></param>
        /// <returns></returns>
        public TagDTO Find(Guid TagId )
        {
            try
            {
                var dbTags = db.Tags.Where(x=>x.Id == TagId).FirstOrDefault();
               
                TagDTO tag = TagMapper.ToDTO(dbTags);
                foreach (var ques in dbTags.Questions)
                {
                  tag.Questions.Add(QuestionMapper.ToDTO(ques));
                }

                return tag;
            }
            catch (NoSuchTagFound)
            {
                throw new NoSuchTagFound();
            }
            catch
            {
                return null;
            }

        }

       
    }
}
