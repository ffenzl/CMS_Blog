using CMS_Blog.SQLite;
using MVCIntegrationExample;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;

namespace CMS_Blog.Models
{
    public partial class Post
    {
        public static readonly string GlobalDateFormat = "yyyy-MM-dd HH:mm:ss";

        public static Collection<Post> GetAll(int blog_id)
        {
            Collection<Post> posts = new Collection<Post>();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
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
                        post.Comments = Comment.GetAll(post.Id, Comment.STATE_APPROVED);
                        post.TitleImage = row.ItemArray[6].ToString();

                        posts.Add(post);
                    }
                }
            }

            return posts;
        } 

        public static Post Get(int post_id)
        {
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                DataTable returnData =
                        database.readSql("select * from Post where post_id = " + post_id);

                if (returnData.Rows.Count > 0)
                {
                    DataRow row = returnData.Rows[0];

                    Post post = new Post();

                    post.Id = Convert.ToInt32(row.ItemArray[0]);
                    post.Text = row.ItemArray[1].ToString();
                    post.Date = Convert.ToDateTime(row.ItemArray[2]);
                    post.User_id = Convert.ToInt32(row.ItemArray[3]);
                    post.Title = row.ItemArray[4].ToString();
                    post.User = User.Get(post.User_id);
                    post.Comments = Comment.GetAll(post.Id, Comment.STATE_APPROVED);
                    post.TitleImage = row.ItemArray[6].ToString();

                    return post;
                }
            }

            return null;
        }


        public static int GetNextRef()
        {
            int nextRef = 1;

            SqlDatabase database = new SqlDatabase();
            database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db"));
            DataTable returnData =
                    database.readSql(
                            "select max(post_id) post_id from Post;");

            if (returnData.Rows.Count > 0 &&
                returnData.Rows[0][0].GetType() != typeof(System.DBNull))
                nextRef = Convert.ToInt32(returnData.Rows[0][0]) + 1;

            return nextRef;
        }
    }
}
