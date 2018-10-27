using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class User
    {
        public User()
        {
            this.FollowCount = 0;
        }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public int FollowCount { get; set; }
    }
}