using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CMS_Blog.Models
{
    public partial class User 
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _alias;
        private string _email;
        private string _password;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Alias
        {
            get { return this._alias; }
            set { this._alias = value; }
        }


        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }


        public string Surname
        {
            get { return this._surname; }
            set { this._surname = value; }
        }


        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }


        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }


    }
}