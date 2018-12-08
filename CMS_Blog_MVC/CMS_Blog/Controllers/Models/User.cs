using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CMS_Blog.Controllers.Models
{
    public class User 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string name)
        {
            this.Name = Name;
        }
    }
}