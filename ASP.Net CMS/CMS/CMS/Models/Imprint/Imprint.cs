using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Blog.Models
{
    public partial class Imprint
    {
        private int _id;
        private string _imprint;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string ImprintText
        {
            get { return this._imprint; }
            set { this._imprint = value; }
        }
    }
}

