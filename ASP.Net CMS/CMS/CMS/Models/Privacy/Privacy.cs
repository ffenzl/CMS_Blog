using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Blog.Models
{
    public partial class Privacy
    {
        private int _id;
        private string _privacyBlob;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string PrivacyBlob
        {
            get { return this._privacyBlob; }
            set { this._privacyBlob = value; }
        }

        //public string Imprint
        //{
        //    get { return this._imprint; }
        //    set { this._imprint = value; }
        //}
    }
}

