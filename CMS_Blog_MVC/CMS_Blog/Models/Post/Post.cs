using System;
using System.Collections.ObjectModel;

namespace CMS_Blog.Models
{
    public partial class Post
    {
        private int _id;

        private string _text;
        private string _title;

        private DateTime _date;
        private User _user;
        private int _user_id;
        private Collection<Comment> _comments;


        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        public DateTime Date
        {
            get { return this._date; }
            set { this._date = value; }
        }

        public User User
        {
            get { return this._user; }
            set { this._user = value; }
        }

        public int User_id
        {
            get { return this._user_id; }
            set { this._user_id = value; }
        }

        public Collection<Comment> Comments
        {
            get { return this._comments; }
            set { this._comments = value; }
        }
    }
}
