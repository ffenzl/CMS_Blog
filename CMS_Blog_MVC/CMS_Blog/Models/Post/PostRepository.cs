using CMS_Blog.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Blog.Models
{
    public partial class Post
    {
        public static Collection<Post> GetAll(int blog_id)
        {
            Collection<Post> posts = new Collection<Post>();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection("SQLite\\CMS_BLOG.db"))
            {
                DataTable returnData = 
                        database.readSql("select * from Post where blog_id = " + blog_id);

                if (returnData.Rows.Count > 0)
                {
                    foreach (DataRow row in returnData.Select())
                    {
                        Post post = new Post();
                        post.Id = Convert.ToInt32(row.ItemArray[0]);
                        post.Text = row.ItemArray[1].ToString();
                        post.Date = Convert.ToDateTime(row.ItemArray[2]);
                        post.User_id = Convert.ToInt32(row.ItemArray[3]);
                        post.Title = row.ItemArray[4].ToString();
                        post.User = User.Get(post.User_id);
                        post.Comments = Comment.GetAll(post.Id);

                        posts.Add(post);
                    }
                }
            }

            return posts;
        } 

        public static Post Get(int post_id)
        {
            return new Post();
        }
    }
}
