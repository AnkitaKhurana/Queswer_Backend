using DataAccess.Data;
using Shared.DTOs;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class TagLogic
    {
        private TagData tagData = new TagData(); 

        /// <summary>
        /// Find Tag by TagId 
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public TagDTO Find(Guid tagId)
        {
            try
            {
                return tagData.Find(tagId);
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

        /// <summary>
        /// Find all Tags 
        /// </summary>
        /// <returns></returns>
        public List<TagDTO> FindAll()
        {
            try
            {
                return tagData.FindAll();
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
