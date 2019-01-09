using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Blog.Models
{
    public partial class Blog
    {
        private int _id;
        private string _title;
        private Collection<Post> _posts;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        public Collection<Post> Posts
        {
            get { return this._posts; }
            set { this._posts = value; }
        }
    }
}
