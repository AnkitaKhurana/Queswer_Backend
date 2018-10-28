using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class Question
    {
        //public Question()
        //{
        //    Tags = new List<Tag>();
        //}
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string Image { get; set; }
        public User Author { get; set; }
        public  List<string> Tags { get; set; }
    }
}