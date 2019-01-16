using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Blog.Models
{
    public partial class Comment
    {
        public const int STATE_OPEN = 0;
        public const int STATE_APPROVED = 1;

        private int _id;
        private int _post_id;
        private string _userName;
        private string _userEmail;
        private DateTime _date;
        private string _text;
        private int _state;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public int Post_id
        {
            get { return this._post_id; }
            set { this._post_id = value; }
        }
        
        public string UserName
        {
            get { return this._userName; }
            set { this._userName = value; }
        }

        public string UserEmail
        {
            get { return this._userEmail; }
            set { this._userEmail = value; }
        }

        public DateTime Date
        {
            get { return this._date; }
            set { this._date = value; }
        }

        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        public int State
        {
            get { return this._state; }
            set { this._state = value; }
        }
    }
}
