using CMS_Blog.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Blog.Models
{
    public partial class Blog
    {
        public static Collection<Blog> GetAll()
        {
            
            return new Collection<Blog>();
        }

        public static Blog Get()
        {
            Blog blog = new Blog();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection("SQLite\\CMS_BLOG.db"))
            {
                DataTable blogData = database.readSql("select * from Blog");

                if (blogData.Rows.Count > 0)
                {
                    foreach (DataRow blogRow in blogData.Select())
                    {
                        blog.Id = Convert.ToInt32(blogRow.ItemArray[0]);
                        blog.Title = blogRow.ItemArray[1].ToString();
                        blog.Posts = Post.GetAll(blog.Id);
                    }
                }
            }

            return blog;
        }

    }
}
