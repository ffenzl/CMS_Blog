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
        private string _privacy;
        private string _imprint;

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

        public string BlogPrivacy
        {
            get { return this._privacy; }
            set { this._privacy = value; }
        }
    }
}
